using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Admin.Panel.Data.Repositories.Questionary
{
    public class QuestionaryObjectTypesRepository: IQuestionaryObjectTypesRepository
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
                    var query = @"SELECT * FROM QuestionaryObjectTypes WHERE Id=@Id";
                    var obj = cn.Query<QuestionaryObjectType>(query, new { @Id = id }).SingleOrDefault();


                    var properties = cn.Query<ObjectProperty>(@"SELECT 
	                                                                p.* 
		                                                                FROM ObjectProperties AS p
			                                                                INNER JOIN ObjectPropertyToObjectTypes AS po ON po.ObjectPropertyId = p.Id
				                                                                where 
					                                                                po.QuestionaryObjectTypeId = @QuestionaryObjectTypeId", new { QuestionaryObjectTypeId = id }).ToList();


                    obj.SelectedObjectProperties = properties;
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
                    var query = "SELECT * FROM QuestionaryObjectTypes";
                    var result = connection.Query<QuestionaryObjectType>(query).ToList();
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
                    var query = "SELECT * FROM QuestionaryObjectTypes WHERE IsUsed = 1";
                    var result = connection.Query<QuestionaryObjectType>(query).ToList();
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
                            @"INSERT INTO QuestionaryObjectTypes(Name, IsUsed) 
                                VALUES(@Name,1);
                                SELECT QuestionaryObjectTypeId = @@IDENTITY";
                        var objTypeId = cn.ExecuteScalar<int>(query, obj, transaction);

                        if (obj.SelectedPropertiesId.Count != 0)
                        {
                            foreach (int objectProperty in obj.SelectedPropertiesId)
                            {
                                cn.Execute(
                                    @"INSERT INTO  ObjectPropertyToObjectTypes(QuestionaryObjectTypeId,ObjectPropertyId)
		                                                VALUES (@QuestionaryObjectTypeId,@ObjectPropertyId)",
                                    new ObjectPropertyToObjectTypes
                                    {
                                        QuestionaryObjectTypeId = objTypeId,
                                        ObjectPropertyId = Convert.ToInt32(objectProperty),
                                    }, transaction);
                            }
                        }

                        var result = cn.Query<QuestionaryObjectType>(@"SELECT * FROM QuestionaryObjectTypes WHERE Id=@Id", new { @Id = objTypeId }, transaction).SingleOrDefault();

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
                connection.Open();
                
                    try
                    {
                        var query = @"UPDATE QuestionaryObjectTypes SET Name=@Name,IsUsed=@IsUsed 
                         WHERE Id=@Id";

                        //дропаем все проперти обьекту
                        connection.Execute(
                            @"DELETE FROM ObjectPropertyToObjectTypes WHERE QuestionaryObjectTypeId = @QuestionaryObjectTypeId",
                            new {QuestionaryObjectTypeId = obj.Id});
                        

                        await connection.ExecuteAsync(query, obj);

                        //добавляем проперти если есть обьекту
                        if (obj.SelectedPropertiesId != null)
                        {
                            foreach (int objectProperty in obj.SelectedPropertiesId)
                            {
                                connection.Execute(
                                    @"INSERT INTO  ObjectPropertyToObjectTypes(QuestionaryObjectTypeId,ObjectPropertyId)
		                                                    VALUES (@QuestionaryObjectTypeId,@ObjectPropertyId)",
                                    new ObjectPropertyToObjectTypes
                                    {
                                        QuestionaryObjectTypeId = obj.Id,
                                        ObjectPropertyId = Convert.ToInt32(objectProperty),
                                    });
                            }   
                        }
                        return obj;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                    }
            }
        }
    }
}
