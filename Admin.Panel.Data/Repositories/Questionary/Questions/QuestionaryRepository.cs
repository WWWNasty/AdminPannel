using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Questions;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Admin.Panel.Data.Repositories.Questionary.Questions
{
    public class QuestionaryRepository : IQuestionaryRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<QuestionaryRepository> _logger;

        public QuestionaryRepository(IConfiguration configuration, ILogger<QuestionaryRepository> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("questionaryConnection");
        }

        public async Task<QuestionaryDto> GetAsync(int id)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();
                try
                {
                    var query = @"SELECT q.*, t.Name AS ObjectTypeName FROM Questionary q 
                                    INNER JOIN QuestionaryObjectTypes t ON q.ObjectTypeId = t.Id 
                                    INNER JOIN Companies c ON q.CompanyId = c.CompanyId
                                    WHERE q.Id=@Id";
                    var obj = cn.Query<QuestionaryDto>(query, new {@Id = id}).SingleOrDefault();

                    List<QuestionaryQuestions> questions = cn.Query<QuestionaryQuestions>(@"SELECT 
	                                                                p.* , f.Name AS QuestionaryInputFieldTypeName, l.Name AS SelectableAnswersListName 
		                                                                FROM QuestionaryQuestions p 
		                                                                INNER JOIN QuestionaryInputFieldTypes f ON f.Id = p.QuestionaryInputFieldTypeId
																		INNER JOIN SelectableAnswersLists l ON l.Id = p.SelectableAnswersListId
				                                                                where QuestionaryId = @QuestionaryId",
                        new {QuestionaryId = id}).ToList();
                    foreach (var question in questions)
                    {
                        var answ = cn.Query<SelectableAnswers>(@"SELECT * FROM SelectableAnswers
				                                                              where SelectableAnswersListId = @SelectableAnswersListId",
                            new {SelectableAnswersListId = question.SelectableAnswersListId}).ToList();
                        question.SelectableAnswers = answ;
                        //инвертирование из можно пропустить вопрос в вопрос обязателен ли
                        question.CanSkipQuestion = !question.CanSkipQuestion;
                    }

                    obj.QuestionaryQuestions = questions;

                    return obj;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Ошибка при получении анкеты {0} в бд: {1}", id, ex);
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<QuestionaryDto>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var query = @"SELECT q.*, c.CompanyName AS CompanyName, o.Name AS ObjectTypeName FROM Questionary q
                    INNER JOIN Companies AS c ON c.CompanyId = q.CompanyId
					INNER JOIN QuestionaryObjectTypes AS o ON o.Id = q.ObjectTypeId";
                    var result = connection.Query<QuestionaryDto>(query).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Ошибка при получении анкет в бд: {0}", ex);
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<QuestionaryDto>> GetAllForUserAsync(string userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    List<QuestionaryDto> questionaries = new List<QuestionaryDto>();
                    List<int> companiesId = connection
                        .Query<int>(@"SELECT c.CompanyId FROM ApplicationUserCompany c WHERE c.UserID = @UserId",
                            new {@UserId = Convert.ToInt32(userId)}).ToList();
                    foreach (var id in companiesId)
                    {
                        var query = @"SELECT * FROM Questionary
                                        WHERE CompanyId =@CompanyId";
                        var objs = connection.Query<QuestionaryDto>(query, new {@CompanyId = id});
                        foreach (var obj in objs)
                        {
                            questionaries.Add(obj);
                        }
                    }

                    return questionaries.ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Ошибка при получении анкет в бд: {0}", ex);
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<QuestionaryDto> CreateAsync(QuestionaryDto selectableAnswersList)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        var query =
                            @"INSERT INTO Questionary(Name, ObjectTypeId, CompanyId, IsUsed) 
                                VALUES(@Name,@ObjectTypeId,@CompanyId,1);
                                SELECT QuestionaryId = @@IDENTITY";
                        var objTypeId = cn.ExecuteScalar<int>(query, selectableAnswersList, transaction);

                        if (selectableAnswersList.QuestionaryQuestions.Count != 0)
                        {
                            foreach (var question in selectableAnswersList.QuestionaryQuestions)
                            {
                                cn.Execute(
                                    @"INSERT INTO  QuestionaryQuestions(QuestionaryId,QuestionText,QuestionaryInputFieldTypeId,CanSkipQuestion,SelectableAnswersListId,SequenceOrder,IsUsed)
		                                                VALUES (@QuestionaryId,@QuestionText,@QuestionaryInputFieldTypeId,@CanSkipQuestion,@SelectableAnswersListId,@SequenceOrder,1)",
                                    new QuestionaryQuestions()
                                    {
                                        QuestionaryId = objTypeId,
                                        QuestionText = question.QuestionText,
                                        QuestionaryInputFieldTypeId = question.QuestionaryInputFieldTypeId,
                                        CanSkipQuestion = !question.CanSkipQuestion,
                                        SelectableAnswersListId = question.SelectableAnswersListId,
                                        SequenceOrder = question.SequenceOrder
                                    }, transaction);
                            }
                        }

                        QuestionaryDto result = cn.Query<QuestionaryDto>(@"SELECT * FROM Questionary WHERE Id=@Id",
                            new {@Id = objTypeId}, transaction).FirstOrDefault();

                        transaction.Commit();
                        _logger.LogInformation("Анкета {0} успешно добавлена в бд для компании с Id: {1}.",
                            selectableAnswersList.Name, selectableAnswersList.CompanyId);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Ошибка при создании анкеты {1} в бд: {0}", ex, selectableAnswersList.Name);
                        throw new Exception($"{GetType().FullName}.WithConnection()", ex);
                    }
                }
            }
        }

        public async Task<QuestionaryDto> UpdateAsync(QuestionaryDto questionary)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var query =
                            @"UPDATE Questionary SET Name=@Name,ObjectTypeId=@ObjectTypeId,CompanyId=@CompanyId,IsUsed=@Isused
                         WHERE Id=@Id";

                        await connection.ExecuteAsync(query, questionary, transaction);

                        List<QuestionaryQuestions> newQuestions = new List<QuestionaryQuestions>();
                        List<QuestionaryQuestions> oldQuestions = new List<QuestionaryQuestions>();

                        foreach (QuestionaryQuestions question in questionary.QuestionaryQuestions)
                        {
                            if (question.Id != 0)
                            {
                                oldQuestions.Add(question);
                            }

                            if (question.Id == 0)
                            {
                                newQuestions.Add(question);
                            }
                        }

                        // редактируем существующие вопросики
                        if (oldQuestions.Count != 0)
                        {
                            foreach (var question in oldQuestions)
                            {
                                connection.Execute(
                                    @"UPDATE QuestionaryQuestions SET QuestionaryId=@QuestionaryId,QuestionText=@QuestionText,QuestionaryInputFieldTypeId=@QuestionaryInputFieldTypeId,CanSkipQuestion=@CanSkipQuestion, 
                                        SelectableAnswersListId=@SelectableAnswersListId,SequenceOrder=@SequenceOrder,IsUsed=@IsUsed
                                        WHERE Id=@Id",
                                    new QuestionaryQuestions
                                    {
                                        Id = question.Id,
                                        QuestionaryId = questionary.Id,
                                        QuestionText = question.QuestionText,
                                        QuestionaryInputFieldTypeId = question.QuestionaryInputFieldTypeId,
                                        CanSkipQuestion = !question.CanSkipQuestion,
                                        SelectableAnswersListId = question.SelectableAnswersListId,
                                        SequenceOrder = question.SequenceOrder,
                                        IsUsed = question.IsUsed
                                    }, transaction);
                            }
                        }

                        //добавляем вопросики анкете
                        if (newQuestions.Count != 0)
                        {
                            foreach (var question in newQuestions)
                            {
                                connection.Execute(
                                    @"INSERT INTO  QuestionaryQuestions(QuestionaryId,QuestionText,QuestionaryInputFieldTypeId,CanSkipQuestion,SelectableAnswersListId,SequenceOrder,IsUsed)
		                                                VALUES (@QuestionaryId,@QuestionText,@QuestionaryInputFieldTypeId,@CanSkipQuestion,@SelectableAnswersListId,@SequenceOrder,@IsUsed)",
                                    new QuestionaryQuestions
                                    {
                                        QuestionaryId = questionary.Id,
                                        QuestionText = question.QuestionText,
                                        QuestionaryInputFieldTypeId = question.QuestionaryInputFieldTypeId,
                                        CanSkipQuestion = !question.CanSkipQuestion,
                                        SelectableAnswersListId = question.SelectableAnswersListId,
                                        SequenceOrder = question.SequenceOrder,
                                        IsUsed = question.IsUsed
                                    }, transaction);
                            }
                        }

                        transaction.Commit();
                        _logger.LogInformation("Анкета с Id :{0} успешно отредактирована в бд.", questionary.Id);
                        return questionary;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Анкета с Id :{0} не отредактирована в бд с ошибкой : {1}.",
                            questionary.Id, ex);
                        throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                    }
                }
            }
        }

        public async Task<bool> IfQuestionaryCurrentInCompanyAsync(int companyId, int typeObjId, int questionaryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    List<QuestionaryDto> questionary = connection.Query<QuestionaryDto>(
                        @"SELECT * FROM Questionary q WHERE q.ObjectTypeId = @ObjecttypeId AND q.CompanyId = @CompanyId AND q.IsUsed = 1",
                        new {@ObjecttypeId = typeObjId, @CompanyId = companyId}).ToList();
                    if (questionary.Count != 0)
                    {
                        foreach (QuestionaryDto q in questionary)
                        {
                            if (q.Id == questionaryId)
                            {
                                return false;
                            }

                            return true;
                        }
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        "Не удалось выполнить проверку на наличие такой активной анкеты в компании c Id:{1} в бд с ошибкой: {0}",
                        ex, companyId);
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }
    }
}