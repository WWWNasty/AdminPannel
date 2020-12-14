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

        public async Task<QueryParameters> GetAllAsync(QueryParameters model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    int pageSize = 50;
                    int pageIndex = model.PageNumber;
                    var query =
                        @";WITH NumberedAnswers AS(
                        SELECT a.Id AS Id, a.CompanyId AS CompanyId, a.CompanyName AS CompanyName, a.Тип AS ObjectType, a.ObjectTypeId AS ObjectTypeId,
                        a.Объект AS ObjectName, a.QuestionaryObjectsId AS ObjectId, a.Описание AS Description, a.Время AS Date, a.Номер AS PhoneNumber, a.Вопрос AS Question,
                        a.Значение AS Answer, a.Комментарий AS Comment, ROW_NUMBER() OVER (ORDER BY a.Время DESC) as RowNumber
	                    FROM vw_Answers AS a";

                    if (model.ObjectIds != null && model.ObjectIds.Length != 0)
                    {
                        model.TotalItems = connection.Query<int>("SELECT COUNT(*) FROM vw_Answers AS a Where a.QuestionaryObjectsId IN @QuestionaryObjectsId", new {@QuestionaryObjectsId =  model.ObjectIds}).FirstOrDefault();
                        var queryString = query + @" WHERE a.QuestionaryObjectsId IN @QuestionaryObjectsId 
                        )
                        SELECT Id, CompanyId, CompanyName, ObjectType, ObjectTypeId, ObjectName, ObjectId, [Description], [Date], PhoneNumber,Question, Answer, Comment
	                    FROM NumberedAnswers a
	                    WHERE RowNumber BETWEEN @skip AND @take";
                        List<CompletedQuestionary> result = connection
                            .Query<CompletedQuestionary>(queryString,
                                new {@QuestionaryObjectsId = model.ObjectIds, @skip = (pageSize * pageIndex) - (pageSize - 1), @take = pageSize * pageIndex})
                            .ToList();
                        model.CompletedQuestionaries = result;
                        model.PageSize = pageSize;
                        return model;
                    }
                    
                    if (model.ObjectTypeIds != null && model.ObjectTypeIds.Length != 0)
                    {
                        model.TotalItems = connection.Query<int>("SELECT COUNT(*) FROM vw_Answers AS a Where a.ObjectTypeId IN @ObjectTypeId", new {@ObjectTypeId = model.ObjectTypeIds}).FirstOrDefault();
                        var queryString = query + @" WHERE a.ObjectTypeId IN @ObjectTypeId 
                        )
                        SELECT Id, CompanyId, CompanyName, ObjectType, ObjectTypeId, ObjectName, ObjectId, [Description], [Date], PhoneNumber,Question, Answer, Comment
	                    FROM NumberedAnswers a
	                    WHERE RowNumber BETWEEN @skip AND @take";
                        List<CompletedQuestionary> result = connection
                            .Query<CompletedQuestionary>(queryString,
                                new {@ObjectTypeId = model.ObjectTypeIds, @skip = (pageSize * pageIndex) - (pageSize - 1), @take = pageSize * pageIndex}).ToList();
                        model.CompletedQuestionaries = result;
                        model.PageSize = pageSize;
                        return model;
                    }
                    
                    if (model.CompanyIds != null && model.CompanyIds.Length != 0)
                    {
                        model.TotalItems = connection.Query<int>("SELECT COUNT(*) FROM vw_Answers AS a Where a.CompanyId IN @CompanyId", new {@CompanyId = model.CompanyIds}).FirstOrDefault();
                        var queryString = query + @" WHERE a.CompanyId IN @CompanyId
                        )
                        SELECT Id, CompanyId, CompanyName, ObjectType, ObjectTypeId, ObjectName, ObjectId, [Description], [Date], PhoneNumber,Question, Answer, Comment
	                    FROM NumberedAnswers a
	                    WHERE RowNumber BETWEEN @skip AND @take";
                        List<CompletedQuestionary> result = connection
                            .Query<CompletedQuestionary>(queryString,
                                new {@CompanyId = model.CompanyIds, @skip = (pageSize * pageIndex) - (pageSize - 1), @take = pageSize * pageIndex}).ToList();
                        model.CompletedQuestionaries = result;
                        model.PageSize = pageSize;
                        return model;
                    }

                    model.TotalItems = 0;
                    model.PageSize = pageSize;
                    return model;
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