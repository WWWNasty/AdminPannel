using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Entities.Questionary.Questions;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Admin.Panel.Data.Repositories.Questionary.Questions
{
    public class SelectableAnswersListRepository : ISelectableAnswersListRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<SelectableAnswersListRepository> _logger;

        public SelectableAnswersListRepository(IConfiguration configuration,
            ILogger<SelectableAnswersListRepository> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("questionaryConnection");
        }

        public async Task<SelectableAnswersLists> GetAsync(int id)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                try
                {
                    var query = @"SELECT * FROM SelectableAnswersLists WHERE Id=@Id";
                    var obj = cn.Query<SelectableAnswersLists>(query, new {@Id = id}).SingleOrDefault();
                    //получение вариантов ответов
                    List<SelectableAnswers> answerses = cn.Query<SelectableAnswers>(@"SELECT * FROM SelectableAnswers 
				                                                                where SelectableAnswersListId = @SelectableAnswersListId
				                                                                order by SequenceOrder asc",
                        new {@SelectableAnswersListId = id}).ToList();

                    obj.SelectableAnswers = answerses;

                    //получение допустимых контроллов 
                    List<QuestionaryInputFieldTypes> inputs = cn.Query<QuestionaryInputFieldTypes>(
                        @"SELECT q.* FROM QuestionaryInputFieldTypes q
                                                                                INNER JOIN  AnswersListInputType qi ON qi.QuestionaryInputFieldTypeId = q.Id
				                                                                where SelectableAnswersListId = @SelectableAnswersListId",
                        new {@SelectableAnswersListId = id}).ToList();

                    obj.SelectedQuestionaryInputFieldTypeses = inputs;

                    return obj;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<SelectableAnswers>> GetSelectableAnswersAsync(int id)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                try
                {
                    List<SelectableAnswers> answerses = cn.Query<SelectableAnswers>(@"SELECT * FROM SelectableAnswers 
				                                                                where SelectableAnswersListId = @SelectableAnswersListId order by SequenceOrder asc",
                        new {SelectableAnswersListId = id}).ToList();

                    return answerses;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<SelectableAnswersLists>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var query = "SELECT * FROM SelectableAnswersLists";
                    var result = connection.Query<SelectableAnswersLists>(query).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<SelectableAnswersLists>> GetAllActiveAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var query = "SELECT * FROM SelectableAnswersLists WHERE IsUsed = 1";
                    var result = connection.Query<SelectableAnswersLists>(query).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<SelectableAnswersLists> CreateAsync(SelectableAnswersLists selectableAnswersList)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();
                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        var query =
                            @"INSERT INTO SelectableAnswersLists(Name, IsUsed) 
                                VALUES(@Name,1);
                                SELECT QuestionaryObjectTypeId = @@IDENTITY";
                        var objTypeId = cn.ExecuteScalar<int>(query, selectableAnswersList, transaction);

                        if (selectableAnswersList.SelectableAnswers.Count != 0)
                        {
                            // добавление вариантов ответа
                            foreach (var answer in selectableAnswersList.SelectableAnswers)
                            {
                                cn.Execute(
                                    @"INSERT INTO  SelectableAnswers(SelectableAnswersListId,AnswerText,SequenceOrder)
		                                                VALUES (@SelectableAnswersListId,@AnswerText,@SequenceOrder)",
                                    new SelectableAnswers
                                    {
                                        SelectableAnswersListId = objTypeId,
                                        AnswerText = answer.AnswerText,
                                        SequenceOrder = answer.SequenceOrder
                                    }, transaction);
                            }
                        }

                        //добавлние допустимых контролов 

                        foreach (var controlId in selectableAnswersList.InputFieldTypesesId)
                        {
                            cn.Execute(
                                @"INSERT INTO  AnswersListInputType(SelectableAnswersListId,QuestionaryInputFieldTypeId)
		                                                VALUES (@SelectableAnswersListId,@QuestionaryInputFieldTypeId)",
                                new AnswersListInputType
                                {
                                    SelectableAnswersListId = objTypeId,
                                    QuestionaryInputFieldTypeId = controlId
                                }, transaction);
                        }

                        var result = cn
                            .Query<SelectableAnswersLists>(@"SELECT * FROM SelectableAnswersLists WHERE Id=@Id",
                                new {@Id = objTypeId}, transaction).SingleOrDefault();

                        transaction.Commit();
                        _logger.LogInformation("Список ответов с Id:{0} успешно добавлен в бд.", objTypeId);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"{GetType().FullName}.WithConnection()", ex);
                    }
                }
            }
        }

        public async Task<SelectableAnswersLists> UpdateAsync(SelectableAnswersLists answersLists)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var query = @"UPDATE SelectableAnswersLists SET Name=@Name,IsUsed=@IsUsed 
                         WHERE Id=@Id";
                        await connection.ExecuteAsync(query, answersLists, transaction);

                        //редактирование списка контроллов 
                        //дропаем контроллы

                        connection.Execute(
                            @"DELETE FROM AnswersListInputType WHERE SelectableAnswersListId = @SelectableAnswersListId",
                            new {SelectableAnswersListId = answersLists.Id}, transaction);

                        //добавляем контроллы
                        if (answersLists.InputFieldTypesesId != null)
                        {
                            foreach (var inputId in answersLists.InputFieldTypesesId)
                            {
                                connection.Execute(
                                    @"INSERT INTO  AnswersListInputType(SelectableAnswersListId,QuestionaryInputFieldTypeId)
                                          		                                                VALUES (@SelectableAnswersListId,@QuestionaryInputFieldTypeId)",
                                    new AnswersListInputType
                                    {
                                        SelectableAnswersListId = answersLists.Id,
                                        QuestionaryInputFieldTypeId = inputId
                                    }, transaction);
                            }
                        }

                        List<SelectableAnswers> oldAnswers = new List<SelectableAnswers>();
                        List<SelectableAnswers> newAnswers = new List<SelectableAnswers>();

                        foreach (var answer in answersLists.SelectableAnswers)
                        {
                            if (answer.Id != 0)
                            {
                                oldAnswers.Add(answer);
                            }

                            if (answer.Id == 0)
                            {
                                newAnswers.Add(answer);
                            }
                        }

                        //редактирование списка ответов
                        if (oldAnswers.Count != 0)
                        {
                            foreach (SelectableAnswers answer in oldAnswers)
                            {
                                connection.Execute(
                                    @"UPDATE SelectableAnswers SET AnswerText=@AnswerText,SequenceOrder=@SequenceOrder 
                                        WHERE Id=@Id",
                                    new SelectableAnswers
                                    {
                                        Id = answer.Id,
                                        SelectableAnswersListId = answersLists.Id,
                                        AnswerText = answer.AnswerText,
                                        SequenceOrder = answer.SequenceOrder
                                    }, transaction);
                            }
                        }

                        //добавляем ответы типу ответов
                        if (newAnswers.Count != 0)
                        {
                            foreach (SelectableAnswers objectProperty in newAnswers)
                            {
                                connection.Execute(
                                    @"INSERT INTO  SelectableAnswers(SelectableAnswersListId,AnswerText,SequenceOrder)
		                                                    VALUES (@SelectableAnswersListId,@AnswerText,@SequenceOrder)",
                                    new SelectableAnswers
                                    {
                                        SelectableAnswersListId = answersLists.Id,
                                        AnswerText = objectProperty.AnswerText,
                                        SequenceOrder = objectProperty.SequenceOrder
                                    }, transaction);
                            }
                        }

                        transaction.Commit();
                        _logger.LogInformation("Список ответов с Id:{0} успешно отредактирован в бд.", answersLists.Id);
                        return answersLists;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                    }
                }
            }
        }
    }
}