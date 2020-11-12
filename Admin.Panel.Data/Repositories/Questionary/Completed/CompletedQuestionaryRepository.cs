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

        public async Task<List<CompletedQuestionary>> GetAllAsync(QueryParameters model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var searchString = "";
                    var query =
                        @" SELECT a.Id AS Id, a.CompanyId AS CompanyId, a.CompanyName AS CompanyName, a.Тип AS ObjectType, a.ObjectTypeId AS ObjectTypeId,
  a.Объект AS ObjectName, a.QuestionaryObjectsId AS ObjectId, a.Описание AS Description, a.Время AS Date, a.Номер AS PhoneNumber, a.Вопрос AS Question,
  a.Значение AS Answer, a.Комментарий AS Comment
  FROM vw_Answers AS a";

                    if (model.CompanyIds != null && model.CompanyIds.Count != 0)
                    {
                        var queryString = query + " WHERE a.CompanyId = @CompanyId";
                        List<CompletedQuestionary> result = new List<CompletedQuestionary>();
                        foreach (var companyId in model.CompanyIds)
                        {
                            var listAnswers = connection.Query<CompletedQuestionary>(queryString, new{@CompanyId = companyId}).ToList();
                            foreach (var answer in listAnswers)
                            {
                                result.Add(answer);
                            }
                        }
                        return result;
                    }

                    if (model.ObjectTypeIds != null && model.ObjectTypeIds.Count != 0)
                    {
                        var queryString = query + " WHERE a.ObjectTypeId = @ObjectTypeId";
                        List<CompletedQuestionary> result = new List<CompletedQuestionary>();
                        foreach (var objTypeId in model.QuestionaryObjectTypes)
                        {
                            var listAnswers = connection.Query<CompletedQuestionary>(queryString, new{@ObjectTypeId = objTypeId}).ToList();
                            foreach (var answer in listAnswers)
                            {
                                result.Add(answer);
                            }
                        }
                        return result;
                    }

                    if (model.ObjectIds != null && model.ObjectIds.Count != 0)
                    {
                        var queryString = query + " WHERE a.QuestionaryObjectsId = @QuestionaryObjectsId";
                        List<CompletedQuestionary> result = new List<CompletedQuestionary>();
                        foreach (var objId in model.ObjectIds)
                        {
                            var listAnswers = connection.Query<CompletedQuestionary>(queryString, new{@QuestionaryObjectsId = objId}).ToList();
                            foreach (var answer in listAnswers)
                            {
                                result.Add(answer);
                            }
                        }
                        return result;
                    }

                    List<CompletedQuestionary> obj = new List<CompletedQuestionary>();
                    return obj;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Ошибка при получении  в бд: {0}", ex);
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }
    }
}