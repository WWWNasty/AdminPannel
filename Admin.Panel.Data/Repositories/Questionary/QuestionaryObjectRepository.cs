using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Data.Exceptions;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Admin.Panel.Data.Repositories.Questionary
{
    public class QuestionaryObjectRepository : IQuestionaryObjectRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<QuestionaryObjectRepository> _logger;

        public QuestionaryObjectRepository(IConfiguration configuration, ILogger<QuestionaryObjectRepository> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("questionaryConnection");
        }

        public async Task<QuestionaryObject> GetAsync(int id)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                try
                {
                    var query = @"SELECT o.*, ot.Name AS ObjectTypeName, c.CompanyName  FROM QuestionaryObjects AS o 
                                          INNER JOIN QuestionaryObjectTypes AS ot ON o.ObjectTypeId = ot.Id
                                          INNER JOIN Companies AS c ON ot.CompanyId = c.CompanyId
                                            WHERE o.Id=@Id";
                    var obj = cn.Query<QuestionaryObject>(query, new {Id = id}).SingleOrDefault();

                    var properties = cn.Query<ObjectPropertyValues>(@"SELECT 
	                                                                po.*, p.IsUsedInReport, p.Name, p.NameInReport, p.ReportCellStyle
		                                                                FROM ObjectPropertyValues AS po
			                                                                INNER JOIN ObjectProperties AS p ON po.ObjectPropertyId = p.Id
				                                                                where 
					                                                                po.QuestionaryObjectId = @QuestionaryObjectId",
                        new {QuestionaryObjectId = id}).ToList();

                    obj.SelectedObjectPropertyValues = properties;

                    return obj;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<QuestionaryObject>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var query = "SELECT * FROM QuestionaryObjects";
                    var result = connection.Query<QuestionaryObject>(query).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<QuestionaryObject>> GetAllActiveAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var query = "SELECT * FROM QuestionaryObjects WHERE IsUsed = 1";
                    var result = connection.Query<QuestionaryObject>(query).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<QuestionaryObject>> GetAllForUserAsync(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    int[] companiesId = connection
                        .Query<int>(@"SELECT c.CompanyId FROM ApplicationUserCompany c WHERE UserID = @Id",
                            new {@Id = userId}).ToArray();
                    int[] objectTypeId = connection
                        .Query<int>(@"SELECT o.Id FROM QuestionaryObjectTypes o WHERE CompanyId IN @CompaniesId",
                            new {@CompaniesId = companiesId}).ToArray();
                    var result = connection
                        .Query<QuestionaryObject>(
                            @"SELECT * FROM QuestionaryObjects WHERE ObjectTypeId IN @ObjectTypeId",
                            new {@ObjectTypeId = objectTypeId}).ToList();

                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<QuestionaryObject>> GetAllActiveForUserAsync(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    int[] companiesId = connection
                        .Query<int>(@"SELECT c.CompanyId FROM ApplicationUserCompany c WHERE UserID = @Id",
                            new {@Id = userId}).ToArray();
                    int[] objectTypeId = connection
                        .Query<int>(@"SELECT o.Id FROM QuestionaryObjectTypes o WHERE CompanyId IN @CompaniesId",
                            new {@CompaniesId = companiesId}).ToArray();
                    var result = connection
                        .Query<QuestionaryObject>(
                            @"SELECT * FROM QuestionaryObjects WHERE IsUsed = 1 AND ObjectTypeId IN @ObjectTypeId",
                            new {@ObjectTypeId = objectTypeId}).ToList();

                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<QuestionaryObject> CreateAsync(QuestionaryObject obj)
        {
            var isCodeUnique = await IsCodeUniqueObjectInQuestionary(obj.Id, obj.Code);
            if (!isCodeUnique)
            {
                throw new CodeObjectNotUniqueException();
            }
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        var query =
                            @"INSERT INTO QuestionaryObjects(Code,Description,Updated,Name,ObjectTypeId,IsUsed) 
                                VALUES(@Code,@Description,@Updated,@Name,@ObjectTypeId,1);
                                SELECT QuestionaryObjectId = @@IDENTITY";
                        var objId = cn.ExecuteScalar<int>(query, obj, transaction);

                        if (obj.SelectedObjectPropertyValues != null)
                        {
                            foreach (ObjectPropertyValues objectProperty in obj.SelectedObjectPropertyValues)
                            {
                                cn.Execute(
                                    @"INSERT INTO  ObjectPropertyValues(QuestionaryObjectId,ObjectPropertyId,Value)
                            		                           VALUES (@QuestionaryObjectId,@ObjectPropertyId,@Value)",
                                    new ObjectPropertyValues
                                    {
                                        QuestionaryObjectId = objId,
                                        ObjectPropertyId = objectProperty.ObjectPropertyId,
                                        Value = objectProperty.Value
                                    }, transaction);
                            }
                        }

                        var result = cn.Query<QuestionaryObject>(@"SELECT * FROM QuestionaryObjects WHERE Id=@Id",
                            new {@Id = objId}, transaction).SingleOrDefault();

                        transaction.Commit();
                        _logger.LogInformation("Объект с Id: {0} успешно добавлен в бд.", objId);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Объект {0} не добавлен в бд с ошибкой: {1}.", obj.Name, ex);
                        throw new Exception($"{GetType().FullName}.WithConnection()", ex);
                    }
                }
            }
        }

        public async Task<QuestionaryObject> UpdateAsync(QuestionaryObject obj)
        {
            var isCodeUnique = await IsCodeUniqueObjectInQuestionary(obj.Id, obj.Code);
            if (!isCodeUnique)
            {
                throw new CodeObjectNotUniqueException();
            }
            
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var query =
                            @"UPDATE QuestionaryObjects SET Code=@Code,Description=@Description,Updated=@Updated,Name=@Name,ObjectTypeId=@ObjectTypeId,IsUsed=@IsUsed 
                                      WHERE Id=@Id";

                        connection.Execute(query, obj, transaction);

                        //дропаем все валью записи объекту
                        connection.Execute(
                            @"DELETE FROM ObjectPropertyValues WHERE QuestionaryObjectId = @QuestionaryObjectId",
                            new {QuestionaryObjectId = obj.Id}, transaction);

                        //добавляем заново все валью записи 
                        if (obj.SelectedObjectPropertyValues != null && obj.SelectedObjectPropertyValues.Count != 0)
                        {
                            foreach (ObjectPropertyValues objectProperty in obj.SelectedObjectPropertyValues)
                            {
                                connection.Execute(
                                    @"INSERT INTO  ObjectPropertyValues(QuestionaryObjectId,ObjectPropertyId,Value)
                                          		                                                VALUES (@QuestionaryObjectId,@ObjectPropertyId,@Value)",
                                    new ObjectPropertyValues
                                    {
                                        QuestionaryObjectId = obj.Id,
                                        ObjectPropertyId = objectProperty.ObjectPropertyId,
                                        Value = objectProperty.Value
                                    }, transaction);
                            }
                        }

                        transaction.Commit();
                        _logger.LogInformation("Объект {0} успешно отредактирован в бд.", obj.Name);
                        return obj;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Объект с Id:{0} не отредактирован в бд с ошибкой: {1}.", obj.Id, ex);
                        throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                    }
                }
            }
        }

        public async Task<List<ObjectPropertyValues>> GetPropertiesForUpdate(int idTypeObj)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var properties = connection.Query<ObjectPropertyValues>(@"SELECT 
                                                                     p.Id AS ObjectPropertyId, p.Name AS Name
                                                                      FROM ObjectProperties AS p
                                                                       INNER JOIN ObjectPropertyToObjectTypes AS po ON po.ObjectPropertyId = p.Id
                                                                        where 
                                                                         po.QuestionaryObjectTypeId = @QuestionaryObjectTypeId",
                        new {QuestionaryObjectTypeId = idTypeObj}).ToList();
                    return properties;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<bool> IsCodeUnique(QuestionaryObject model)
        {
            return await IsCodeObjectUnique(model.Id, model.Code);
        }

        public async Task<bool> IsCodeUniqueObjectInQuestionary(int idObject, string code)
        {
            return await IsCodeObjectUnique(idObject, code);
        }

        private async Task<bool> IsCodeObjectUnique(int idObject, string code)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var obj = connection.Query<QuestionaryObject>(
                        @"SELECT * FROM QuestionaryObjects Where Code =@Code",
                        new {@Code = code}).ToList();
                    if (idObject == 0)
                    {
                        return obj.Count == 0;
                    }

                    var objs = connection.Query<QuestionaryObject>(
                        @"SELECT * FROM QuestionaryObjects Where Code =@Code AND Id <> @Id",
                        new {@Code = code, @Id = idObject}).ToList();
                    return objs.Count == 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }
    }
}