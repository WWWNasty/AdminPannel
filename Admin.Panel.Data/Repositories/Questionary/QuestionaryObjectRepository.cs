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
                    var query = @"SELECT * FROM QuestionaryObjects WHERE Id=@Id";
                    var obj = cn.Query<QuestionaryObject>(query, new { @Id = id }).SingleOrDefault();
                    //TODO нужна дто для получения отдельная
                    //var values = cn.Query<ObjectPropertyValues>(@"SELECT* FROM ObjectPropertyValues AS v
                    //INNER JOIN ObjectProperty AS p ON v.ObjectPropertyId = p.Id
                    //INNER JOIN QuestionaryObjects AS o ON v.QuestionaryObjectId = o.Id", new { @Id = id }).ToList();


                    //var properties = cn.Query<QuestionaryObjectType>(@"SELECT* FROM ObjectPropertyValues AS v
                    //INNER JOIN ObjectProperty AS p ON v.ObjectPropertyId = p.Id
                    //INNER JOIN QuestionaryObjects AS o ON v.QuestionaryObjectId = o.Id", new { @Id = id }).ToList();

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
                    var query = "SELECT * FROM Companies";
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
                        var value = cn.ExecuteScalar<int>(query, obj, transaction);

                        var objId = value;
                        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                        List<ObjectProperty> objectProperties = cn.Query<ObjectProperty>(@"SELECT * FROM ObjectProperties AS p 
                            INNER JOIN ObjectPropertyToObjectTypes AS tp ON tp.ObjectPropertyId = p.Id
                            INNER JOIN QuestionaryObjectTypes AS t ON tp.QuestionaryObjectTypeId = t.Id").ToList();

                        foreach (ObjectProperty objectProperty in objectProperties)
                        {
                            cn.Execute(@"INSERT INTO  ObjectPropertyValues(QuestionaryObjectId,ObjectPropertyId,Value)
		                                                VALUES (@QuestionaryObjectId,@ObjectPropertyId,@Value)",
                                new ObjectPropertyValues
                                {
                                    QuestionaryObjectId = objId,
                                    ObjectPropertyId = objectProperty.Id,
                                    Value = obj.Value
                                }, transaction);

                        }

                        var result = cn.Query<QuestionaryObject>(@"SELECT * FROM QuestionaryObjects WHERE Id=@Id", new { @Id = value }).SingleOrDefault();

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
    }
}
