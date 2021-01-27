using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.Questionary.Questions;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Admin.Panel.Data.Repositories.Questionary.Questions
{
    public class QuestionaryInputFieldsTypesRepository : IQuestionaryInputFieldTypesRepository
    {
        private readonly string _connectionString;

        public QuestionaryInputFieldsTypesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("questionaryConnection");
        }

        public async Task<List<QuestionaryInputFieldTypes>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var query = "SELECT * FROM QuestionaryInputFieldTypes";
                    var result = await connection.QueryAsync<QuestionaryInputFieldTypes>(query);
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<QuestionaryInputFieldTypes>> GetAllWithListId()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var query = @"SELECT i.*, ai.SelectableAnswersListId as SelectableAnswersListId FROM QuestionaryInputFieldTypes i
                    INNER JOIN AnswersListInputType ai ON i.Id = ai.QuestionaryInputFieldTypeId";
                    var result =
                        await connection.QueryAsync<QuestionaryInputFieldTypes>(query);
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<QuestionaryInputFieldTypes>> GetAllCurrent(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var query = @"SELECT i.* FROM QuestionaryInputFieldTypes i
                    INNER JOIN AnswersListInputType ai ON i.Id = ai.QuestionaryInputFieldTypeId
                    WHERE ai.SelectableAnswersListId = @SelectableAnswersListId";
                    var result =
                        await connection.QueryAsync<QuestionaryInputFieldTypes>(query,
                            new {@SelectableAnswersListId = id});
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