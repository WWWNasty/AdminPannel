using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.Questionary;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Admin.Panel.Data.Repositories.Questionary
{
    public class QuestionaryObjectRepository: IQuestionaryObjectRepository
    {
        private readonly string _connectionString;

        public QuestionaryObjectRepository(IConfiguration configuration)
        {
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
                                          INNER JOIN Companies AS c ON o.CompanyId = c.CompanyId
                                            WHERE o.Id=@Id";
                    var obj = cn.Query<QuestionaryObject>(query, new { @Id = id }).SingleOrDefault();

                    var properties = cn.Query<ObjectPropertyValues>(@"SELECT 
	                                                                po.*, p.IsUsedInReport, p.Name, p.NameInReport, p.ReportCellStyle
		                                                                FROM ObjectPropertyValues AS po
			                                                                INNER JOIN ObjectProperties AS p ON po.ObjectPropertyId = p.Id
				                                                                where 
					                                                                po.QuestionaryObjectId = @QuestionaryObjectId", new { QuestionaryObjectId = id }).ToList();
                    

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

        public async Task<QuestionaryObject> CreateAsync(QuestionaryObject obj)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        var query =
                            @"INSERT INTO QuestionaryObjects(Code,Description,Updated,Name,ObjectTypeId,CompanyId) 
                                VALUES(@Code,@Description,@Updated,@Name,@ObjectTypeId,@CompanyId);
                                SELECT QuestionaryObjectId = @@IDENTITY";
                        var objId = cn.ExecuteScalar<int>(query, obj, transaction);
                        
                        // List<ObjectProperty> objectProperties = cn.Query<ObjectProperty>(@"SELECT p.* FROM ObjectProperties AS p 
                        //     INNER JOIN ObjectPropertyToObjectTypes AS tp ON tp.ObjectPropertyId = p.Id
                        //     WHERE tp.QuestionaryObjectTypeId = @QuestionaryObjectTypeId ",new { QuestionaryObjectTypeId = obj.ObjectTypeId }, transaction).ToList();

                        foreach (ObjectPropertyValues objectProperty in obj.SelectedObjectPropertyValues)
                        {
                            cn.Execute(@"INSERT INTO  ObjectPropertyValues(QuestionaryObjectId,ObjectPropertyId,Value)
		                                                VALUES (@QuestionaryObjectId,@ObjectPropertyId,@Value)",
                                new ObjectPropertyValues
                                {
                                    QuestionaryObjectId = objId,
                                    ObjectPropertyId = objectProperty.Id,
                                    Value = objectProperty.Value
                                }, transaction);

                        }

                        var result = cn.Query<QuestionaryObject>(@"SELECT * FROM QuestionaryObjects WHERE Id=@Id", new { @Id = objId }, transaction).SingleOrDefault();

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

        public async Task<QuestionaryObject> UpdateAsync(QuestionaryObject obj)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    var query = @"UPDATE QuestionaryObjects SET Code=@Code,Description=@Description,Updated=@Updated,Name=@Name,ObjectTypeId=@ObjectTypeId,CompanyId=@CompanyId 
                    WHERE Id=@Id";
                        //достать все id выбранных проперти и все записи с данным id объекта из таблицы с велью, и все записи без id выбранных проперти дропать
                    //TODO нужно додумать логику удаления из ObjectPropertyValues если изменен QuestionaryObjectTypes и добавление новых values в QuestionaryObjectTypes
                    await connection.ExecuteAsync(query, obj);
                    return obj;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
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
                                                                     p.* 
                                                                      FROM ObjectProperties AS p
                                                                       INNER JOIN ObjectPropertyToObjectTypes AS po ON po.ObjectPropertyId = p.Id
                                                                        where 
                                                                         po.QuestionaryObjectTypeId = @QuestionaryObjectTypeId", new { QuestionaryObjectTypeId = idTypeObj }).ToList();
                    return properties;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public Task<List<ObjectPropertyValues>> GetPropertiesForCreate(int id)
        {
            throw new NotImplementedException();
        }
    }
}
