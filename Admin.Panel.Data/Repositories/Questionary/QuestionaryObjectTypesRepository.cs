using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Admin.Panel.Data.Repositories.Questionary
{
    public class QuestionaryObjectTypesRepository : IQuestionaryObjectTypesRepository
    {
        private readonly string _connectionString;

        public QuestionaryObjectTypesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("questionaryConnection");
        }

        public async Task<QuestionaryObjectType> GetAsync(int id)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();
                try
                {
                    var query = @"SELECT t.*, c.CompanyName AS CompanyName  FROM QuestionaryObjectTypes AS t 
																			INNER JOIN Companies AS c ON c.CompanyId = t.CompanyId
				                                                               WHERE t.Id=@Id";
                    var obj = cn.Query<QuestionaryObjectType>(query, new {@Id = id}).SingleOrDefault();


                    var properties = cn.Query<ObjectProperty>(@"SELECT 
	                                                                p.* 
		                                                                FROM ObjectProperties AS p
			                                                                INNER JOIN ObjectPropertyToObjectTypes AS po ON po.ObjectPropertyId = p.Id
				                                                                where 
					                                                                po.QuestionaryObjectTypeId = @QuestionaryObjectTypeId",
                        new {QuestionaryObjectTypeId = id}).ToList();

                    obj.ObjectProperties = properties.Count == 0 ? new List<ObjectProperty>() {new ObjectProperty()} : properties;
                    return obj;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<QuestionaryObjectType>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var query = @"SELECT t.*, c.CompanyName AS CompanyName  FROM QuestionaryObjectTypes AS t 
                    INNER JOIN Companies AS c ON c.CompanyId = t.CompanyId";
                    var result = connection.Query<QuestionaryObjectType>(query).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<QuestionaryObjectType>> GetAllForUserAsync(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    int[] companiesId = connection
                        .Query<int>(@"SELECT c.CompanyId FROM ApplicationUserCompany c WHERE UserID = @Id",
                            new {@Id = userId}).ToArray();

                    var query = @"SELECT t.*, c.CompanyName AS CompanyName  FROM QuestionaryObjectTypes AS t 
                    INNER JOIN Companies AS c ON c.CompanyId = t.CompanyId WHERE t.CompanyId IN @CompanyId";
                    var result = connection.Query<QuestionaryObjectType>(query, new {CompanyId = companiesId}).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<QuestionaryObjectType>> GetAllActiveAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var query = @"SELECT t.*, c.CompanyName AS CompanyName  FROM QuestionaryObjectTypes AS t 
                    INNER JOIN Companies AS c ON c.CompanyId = t.CompanyId WHERE t.IsUsed = 1";
                    var result = connection.Query<QuestionaryObjectType>(query).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<QuestionaryObjectType>> GetAllActiveForUserAsync(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    int[] companiesId = connection
                        .Query<int>(@"SELECT c.CompanyId FROM ApplicationUserCompany c WHERE UserID = @Id",
                            new {@Id = userId}).ToArray();

                    var query = @"SELECT t.*, c.CompanyName AS CompanyName  FROM QuestionaryObjectTypes AS t 
                    INNER JOIN Companies AS c ON c.CompanyId = t.CompanyId WHERE t.IsUsed = 1 AND t.CompanyId IN @CompanyId";
                    var result = connection.Query<QuestionaryObjectType>(query, new {CompanyId = companiesId}).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<QuestionaryObjectType> CreateAsync(QuestionaryObjectType obj)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        var query =
                            @"INSERT INTO QuestionaryObjectTypes(Name, IsUsed, CompanyId) 
                                VALUES(@Name,1, @CompanyId);
                                SELECT QuestionaryObjectTypeId = @@IDENTITY";
                        var objTypeId = cn.ExecuteScalar<int>(query, obj, transaction);

                        if (obj.ObjectProperties != null)
                        {
                            foreach (ObjectProperty objectProperty in obj.ObjectProperties)
                            {
                                //создание свойств
                                var objectPropertyId = cn.ExecuteScalar<int>(
                                    @"INSERT INTO ObjectProperties(Name,NameInReport,IsUsedInReport,ReportCellStyle,IsUsed) 
                                            VALUES(@Name,@NameInReport,@IsUsedInReport,'Text',1)
                                            SELECT QuestionaryObjectTypeId = @@IDENTITY",
                                    objectProperty, transaction);

                                // добавление свойств типу объекта
                                cn.Execute(
                                    @"INSERT INTO  ObjectPropertyToObjectTypes(QuestionaryObjectTypeId,ObjectPropertyId)
		                                                VALUES (@QuestionaryObjectTypeId,@ObjectPropertyId)",
                                    new ObjectPropertyToObjectTypes
                                    {
                                        QuestionaryObjectTypeId = objTypeId,
                                        ObjectPropertyId = objectPropertyId,
                                    }, transaction);
                            }
                        }

                        var result = cn.Query<QuestionaryObjectType>(
                            @"SELECT * FROM QuestionaryObjectTypes WHERE Id=@Id",
                            new {@Id = objTypeId}, transaction).SingleOrDefault();

                        transaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"{GetType().FullName}.WithConnection()", ex);
                    }
                }
            }
        }

        public async Task<QuestionaryObjectType> UpdateAsync(QuestionaryObjectType obj)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var query = @"UPDATE QuestionaryObjectTypes SET Name=@Name,IsUsed=@IsUsed,CompanyId=@CompanyId 
                         WHERE Id=@Id";
                        await connection.ExecuteAsync(query, obj, transaction);

                        List<ObjectProperty> newProperties = new List<ObjectProperty>();
                        List<ObjectProperty> oldProperties = new List<ObjectProperty>();

                        foreach (var property in obj.ObjectProperties)
                        {
                            if (property.Id != 0)
                            {
                                oldProperties.Add(property);
                            }
                            else
                            {
                                newProperties.Add(property);
                            }
                        }

                        foreach (ObjectProperty objectProperty in oldProperties)
                        {
                            connection.Execute(
                                @"UPDATE ObjectProperties SET Name=@Name,NameInReport=@NameInReport,IsUsedInReport=@IsUsedInReport,ReportCellStyle='Text',IsUsed=@IsUsed 
                                        WHERE Id=@Id",
                                objectProperty, transaction);
                        }

                        foreach (ObjectProperty objectProperty in newProperties)
                        {
                            //создание свойств
                            var objectPropertyId = connection.ExecuteScalar<int>(
                                @"INSERT INTO ObjectProperties(Name,NameInReport,IsUsedInReport,ReportCellStyle,IsUsed) 
                                            VALUES(@Name,@NameInReport,@IsUsedInReport,'Text',@IsUsed)
                                            SELECT QuestionaryObjectTypeId = @@IDENTITY",
                                objectProperty, transaction);

                            // добавление свойств типу объекта
                            connection.Execute(
                                @"INSERT INTO  ObjectPropertyToObjectTypes(QuestionaryObjectTypeId,ObjectPropertyId)
		                                                VALUES (@QuestionaryObjectTypeId,@ObjectPropertyId)",
                                new ObjectPropertyToObjectTypes
                                {
                                    QuestionaryObjectTypeId = obj.Id,
                                    ObjectPropertyId = objectPropertyId,
                                }, transaction);
                        }


                        transaction.Commit();

                        return obj;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                    }
                }
            }
        }

        public async Task<List<QuestionaryObjectType>> GetAllCurrent(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var query = @"SELECT * FROM QuestionaryObjectTypes WHERE CompanyId = @CompanyId AND IsUsed = 1";
                    var result =
                         connection.Query<QuestionaryObjectType>(query,
                            new {@CompanyId = id});
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }
    }
}