using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Questions;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Admin.Panel.Data.Repositories.Questionary.Questions
{
    public class QuestionaryRepository: IQuestionaryRepository
    {
        private readonly string _connectionString;

        public QuestionaryRepository(IConfiguration configuration)
        {
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
                    var obj = cn.Query<QuestionaryDto>(query, new { @Id = id }).SingleOrDefault();
                    
                    List<QuestionaryQuestions> questions = cn.Query<QuestionaryQuestions>(@"SELECT 
	                                                                p.* , f.Name AS QuestionaryInputFieldTypeName, l.Name AS SelectableAnswersListName 
		                                                                FROM QuestionaryQuestions p 
		                                                                INNER JOIN QuestionaryInputFieldTypes f ON f.Id = p.QuestionaryInputFieldTypeId
																		INNER JOIN SelectableAnswersLists l ON l.Id = p.SelectableAnswersListId
				                                                                where QuestionaryId = @QuestionaryId", new { QuestionaryId = id }).ToList();
                    foreach (var question in questions)
                    {
                        var answ = cn.Query<SelectableAnswers>(@"SELECT * FROM SelectableAnswers
				                                                              where SelectableAnswersListId = @SelectableAnswersListId", new { SelectableAnswersListId = question.SelectableAnswersListId }).ToList();
                        question.SelectableAnswers = answ;  
                    }

                    obj.QuestionaryQuestions = questions;

                    return obj;
                }
                catch (Exception ex)
                {
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
                    var query = "SELECT * FROM Questionary";
                    var result = connection.Query<QuestionaryDto>(query).ToList();
                    return result;
                }
                catch (Exception ex)
                {
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
                    List<int> companiesId =  connection.Query<int>(@"SELECT c.CompanyId FROM ApplicationUserCompany c WHERE c.UserID = @UserId", new {@UserId = Convert.ToInt32(userId)}).ToList();
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
                            @"INSERT INTO Questionary(Name, ObjectTypeId, CompanyId) 
                                VALUES(@Name,@ObjectTypeId,@CompanyId);
                                SELECT QuestionaryId = @@IDENTITY";
                        var objTypeId = cn.ExecuteScalar<int>(query, selectableAnswersList, transaction);

                        if (selectableAnswersList.QuestionaryQuestions.Count != 0)
                        {
                            foreach (var question  in selectableAnswersList.QuestionaryQuestions)
                            {
                                cn.Execute(
                                    @"INSERT INTO  QuestionaryQuestions(QuestionaryId,QuestionText,QuestionaryInputFieldTypeId,CanSkipQuestion,SelectableAnswersListId,SequenceOrder,IsUsed)
		                                                VALUES (@QuestionaryId,@QuestionText,@QuestionaryInputFieldTypeId,@CanSkipQuestion,@SelectableAnswersListId,@SequenceOrder,1)",
                                    new QuestionaryQuestions()
                                    {
                                       QuestionaryId = objTypeId,
                                       QuestionText = question.QuestionText,
                                       QuestionaryInputFieldTypeId = question.QuestionaryInputFieldTypeId,
                                       CanSkipQuestion = question.CanSkipQuestion,
                                       SelectableAnswersListId = question.SelectableAnswersListId,
                                       SequenceOrder = question.SequenceOrder
                                    }, transaction);
                            }
                        }

                        QuestionaryDto result = cn.Query<QuestionaryDto>(@"SELECT * FROM Questionary WHERE Id=@Id", new { @Id = objTypeId }, transaction).FirstOrDefault();

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

        public async Task<QuestionaryDto> UpdateAsync(QuestionaryDto questionary)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                try
                {
                    var query = @"UPDATE Questionary SET Name=@Name,ObjectTypeId=@ObjectTypeId 
                         WHERE Id=@Id";

                    await connection.ExecuteAsync(query, questionary);
                    
                    //дропаем все вопросики анкете
                    connection.Execute(
                        @"DELETE FROM QuestionaryQuestions WHERE QuestionaryId = @QuestionaryId",
                        new {QuestionaryId = questionary.Id});

                    //добавляем вопросики анкете
                    if (questionary.QuestionaryQuestions.Count != 0)
                    {
                        foreach (var question  in questionary.QuestionaryQuestions)
                        {
                            connection.Execute(
                                @"INSERT INTO  QuestionaryQuestions(QuestionaryId,QuestionText,QuestionaryInputFieldTypes,CanSkipQuestion,SelectebleAnswersListId,SequenceOrder,IsUsed)
		                                                VALUES (@QuestionaryId,@QuestionText,@QuestionaryInputFieldTypeId,@CanSkipQuestion,@SelectableAnswersListId,@SequenceOrder,1)",
                                new QuestionaryQuestions()
                                {
                                    QuestionaryId = questionary.Id,
                                    QuestionText = question.QuestionText,
                                    QuestionaryInputFieldTypeId = question.QuestionaryInputFieldTypeId,
                                    CanSkipQuestion = question.CanSkipQuestion,
                                    SelectableAnswersListId = question.SelectableAnswersListId,
                                    SequenceOrder = question.SequenceOrder
                                });
                        }
                    }
                    return questionary;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }
    }
}