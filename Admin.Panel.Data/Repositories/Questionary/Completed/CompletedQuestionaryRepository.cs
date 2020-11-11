using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Completed;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.Completed;
using Admin.Panel.Data.Repositories.Questionary.Questions;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Admin.Panel.Data.Repositories.Questionary.Completed
{
    public class CompletedQuestionaryRepository : ICompletedQuestionaryRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<QuestionaryRepository> _logger;

        public CompletedQuestionaryRepository(IConfiguration configuration, ILogger<QuestionaryRepository> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("questionaryConnection");
        }

        public async Task<List<CompletedQuestionary>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var query =
                        @"SELECT a.Id AS Id, a.CompanyId AS CompanyId, a.CompanyName AS CompanyName, a.Тип AS ObjectType, 
  a.Объект AS ObjectName, a.Описание AS Description, a.Время AS Date, a.Номер AS PhoneNumber, a.Вопрос AS Question,
  a.Значение AS Answer, a.Комментарий AS Comment
  FROM vw_Answers AS a";
                    var result = connection.Query<CompletedQuestionary>(query).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Ошибка при получении анкет в бд: {0}", ex);
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }
    }
}