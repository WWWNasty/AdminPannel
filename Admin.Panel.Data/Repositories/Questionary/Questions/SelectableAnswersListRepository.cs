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

namespace Admin.Panel.Data.Repositories.Questionary.Questions
{
    public class SelectableAnswersListRepository: ISelectableAnswersListRepository
    {
        private readonly string _connectionString;

        public SelectableAnswersListRepository(IConfiguration configuration)
        {
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
                    var obj = cn.Query<SelectableAnswersLists>(query, new { @Id = id }).SingleOrDefault();
                    List<SelectableAnswers> answerses = cn.Query<SelectableAnswers>(@"SELECT * FROM SelectableAnswers 
				                                                                where SelectableAnswersListId = @SelectableAnswersListId", 
                        new { @SelectableAnswersListId = id }).ToList();

                    obj.SelectableAnswers = answerses;
                    return obj;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<SelectableAnswers[]> GetSelectableAnswersAsync(int id)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                try
                {
                    SelectableAnswers[] answerses = cn.Query<SelectableAnswers>(@"SELECT * FROM SelectableAnswers 
				                                                                where SelectableAnswersListId = @SelectableAnswersListId", 
                        new { @SelectableAnswersListId = id }).ToArray();
                    
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
                            foreach (var answer in selectableAnswersList.SelectableAnswers)
                            {
                                cn.Execute(
                                    @"INSERT INTO  SelectableAnswers(SelectableAnswersListId,AnswerText,IsDefaultAnswer,IsInvolvesComment,SequenceOrder)
		                                                VALUES (@SelectableAnswersListId,@AnswerText,@IsDefaultAnswer,@IsInvolvesComment,@SequenceOrder)",
                                    new SelectableAnswers()
                                    {
                                        SelectableAnswersListId = objTypeId,
                                        AnswerText = answer.AnswerText,
                                        IsDefaultAnswer = answer.IsDefaultAnswer,
                                        IsInvolvesComment = answer.IsInvolvesComment,
                                        SequenceOrder = answer.SequenceOrder
                                    }, transaction);
                            }
                        }

                        var result = cn.Query<SelectableAnswersLists>(@"SELECT * FROM SelectableAnswersLists WHERE Id=@Id", new { @Id = objTypeId }, transaction).SingleOrDefault();

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

        public async Task<SelectableAnswersLists> UpdateAsync(SelectableAnswersLists answersLists)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                try
                {
                    var query = @"UPDATE SelectableAnswersLists SET Name=@Name,IsUsed=@IsUsed 
                         WHERE Id=@Id";
                    await connection.ExecuteAsync(query, answersLists);
                    
                    // //дропаем все ответы нельзя дропать свзаны с записями в бд TODO пеерделать
                    // connection.Execute(
                    //     @"DELETE FROM SelectableAnswers WHERE SelectableAnswersListId = @SelectableAnswersListId",
                    //     new {SelectableAnswersListId = answersLists.Id});
 
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
                    if (oldAnswers != null)
                    {
                        foreach (SelectableAnswers answer in oldAnswers)
                        {
                            connection.Execute(
                                @"UPDATE SelectableAnswers SET AnswerText=@AnswerText,IsDefaultAnswer=@IsDefaultAnswer,IsInvolvesComment=@IsInvolvesComment,SequenceOrder=@SequenceOrder 
                                        WHERE SelectableAnswersListId=@SelectableAnswersListId",
                                new SelectableAnswers
                                {
                                    SelectableAnswersListId = answersLists.Id,
                                    AnswerText = answer.AnswerText,
                                    IsDefaultAnswer = answer.IsDefaultAnswer,
                                    IsInvolvesComment = answer.IsInvolvesComment,
                                    SequenceOrder = answer.SequenceOrder
                                });
                        }   
                    }
                    
                    //добавляем ответы типу ответов
                    if (newAnswers != null)
                    {
                        foreach (SelectableAnswers objectProperty in newAnswers)
                        {
                            connection.Execute(
                                @"INSERT INTO  SelectableAnswers(SelectableAnswersListId,AnswerText,IsDefaultAnswer,IsInvolvesComment,SequenceOrder)
		                                                    VALUES (@SelectableAnswersListId,@AnswerText,@IsDefaultAnswer,@IsInvolvesComment,@SequenceOrder)",
                                new SelectableAnswers
                                {
                                    SelectableAnswersListId = answersLists.Id,
                                    AnswerText = objectProperty.AnswerText,
                                    IsDefaultAnswer = objectProperty.IsDefaultAnswer,
                                    IsInvolvesComment = objectProperty.IsInvolvesComment,
                                    SequenceOrder = objectProperty.SequenceOrder
                                });
                        }   
                    }
                   
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