using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Entities.Questionary.Questions;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces;

namespace Admin.Panel.Core.Services.QuestionaryServices.QuestionsServices
{
    public class QuestionaryService : IQuestionaryService
    {
        private readonly IQuestionaryObjectTypesRepository _questionaryObjectTypesRepository;
        private readonly ISelectableAnswersListRepository _selectableAnswersListRepository;
        private readonly IQuestionaryInputFieldTypesRepository _questionaryInputFieldTypesRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IQuestionaryRepository _questionaryRepository;
        private readonly IQuestionaryInputFieldTypesRepository _fieldTypesRepository;

        public QuestionaryService(
            IQuestionaryObjectTypesRepository questionaryObjectTypesRepository,
            ISelectableAnswersListRepository selectableAnswersListRepository,
            IQuestionaryInputFieldTypesRepository questionaryInputFieldTypesRepository,
            ICompanyRepository companyRepository,
            IQuestionaryRepository questionaryRepository, 
            IQuestionaryInputFieldTypesRepository fieldTypesRepository)
        {
            _questionaryObjectTypesRepository = questionaryObjectTypesRepository;
            _selectableAnswersListRepository = selectableAnswersListRepository;
            _questionaryInputFieldTypesRepository = questionaryInputFieldTypesRepository;
            _companyRepository = companyRepository;
            _questionaryRepository = questionaryRepository;
            _fieldTypesRepository = fieldTypesRepository;
        }

        private async Task<QuestionaryDto> GetAllForQuestionary()
        {
            List<QuestionaryObjectType> objectTypes = await _questionaryObjectTypesRepository.GetAllActiveAsync();
            List<SelectableAnswersLists> answersListTypes = await _selectableAnswersListRepository.GetAllActiveAsync();
            List<ApplicationCompany> companies = await _companyRepository.GetAllActiveAsync();
            QuestionaryDto obj = new QuestionaryDto();
            obj.ApplicationCompanies = companies;
            obj.QuestionaryObjectTypes = objectTypes;
            obj.SelectableAnswersLists = answersListTypes;
            return obj;
        }

        private async Task<QuestionaryDto> GetAllForQuestionaryUser(string idUser)
        {
            List<QuestionaryObjectType> objectTypes = await _questionaryObjectTypesRepository.GetAllActiveForUserAsync(Convert.ToInt32(idUser));
            List<SelectableAnswersLists> answersListTypes = await _selectableAnswersListRepository.GetAllActiveAsync();
            List<ApplicationCompany> companies = await _companyRepository.GetAllActiveForUserAsync(idUser);
            QuestionaryDto obj = new QuestionaryDto();
            obj.ApplicationCompanies = companies;
            obj.QuestionaryObjectTypes = objectTypes;
            obj.SelectableAnswersLists = answersListTypes;
            return obj;
        }

        public async Task<QuestionaryDto> GetAllForQuestionaryUpdate(QuestionaryDto model)
        {
            var obj = await GetAllForQuestionary();
            model.ApplicationCompanies = obj.ApplicationCompanies;
            model.QuestionaryObjectTypes = obj.QuestionaryObjectTypes;
            model.SelectableAnswersLists = obj.SelectableAnswersLists;
            if (model.QuestionaryQuestions != null)
            {
                foreach (QuestionaryQuestions question in model.QuestionaryQuestions)
                {
                    if (question.SelectableAnswersListId != 0)
                    {
                        List<QuestionaryInputFieldTypes> currentInputFields =
                            await _questionaryInputFieldTypesRepository.GetAllCurrent(question.SelectableAnswersListId);
                        question.CurrentQuestionaryInputFieldTypes = currentInputFields;
                  
                        List<SelectableAnswers> current =
                            await _selectableAnswersListRepository.GetSelectableAnswersAsync(question.SelectableAnswersListId);
                        question.CurrentSelectableAnswerses = current;
                    }
                }
            }
            return model;
        }

        public async Task<QuestionaryDto> GetAllForQuestionaryCreate(QuestionaryDto model)
        {
            var allForQuestionary = await GetAllForQuestionary();
            model.ApplicationCompanies = allForQuestionary.ApplicationCompanies;
            model.QuestionaryObjectTypes = allForQuestionary.QuestionaryObjectTypes;
            model.SelectableAnswersLists = allForQuestionary.SelectableAnswersLists;
            if (model.QuestionaryQuestions != null)
            {
                foreach (QuestionaryQuestions question in model.QuestionaryQuestions)
                {
                    if (question.SelectableAnswersListId != 0)
                    {
                        List<QuestionaryInputFieldTypes> currentInputFields =
                            await _questionaryInputFieldTypesRepository.GetAllCurrent(question.SelectableAnswersListId);
                        question.CurrentQuestionaryInputFieldTypes = currentInputFields;
                        List<SelectableAnswers> current =
                            await _selectableAnswersListRepository.GetSelectableAnswersAsync(question.SelectableAnswersListId);
                        question.CurrentSelectableAnswerses = current;
                    }
                }
            }
            return model;
        }

        public async Task<QuestionaryDto> GetAllForQuestionaryForUserUpdate(QuestionaryDto model, string idUser)
        {
            var allForObj = await GetAllForQuestionaryUser(idUser);
            model.ApplicationCompanies = allForObj.ApplicationCompanies;
            model.SelectableAnswersLists = allForObj.SelectableAnswersLists;
            if (model.QuestionaryQuestions != null)
            {
                foreach (QuestionaryQuestions question in model.QuestionaryQuestions)
                {
                    if (question.SelectableAnswersListId != 0)
                    {
                        List<QuestionaryInputFieldTypes> currentInputFields =
                            await _questionaryInputFieldTypesRepository.GetAllCurrent(question.SelectableAnswersListId);
                        question.CurrentQuestionaryInputFieldTypes = currentInputFields;
                        List<SelectableAnswers> current =
                            await _selectableAnswersListRepository.GetSelectableAnswersAsync(question.SelectableAnswersListId);
                        question.CurrentSelectableAnswerses = current;
                    }
                }
            }
            model.QuestionaryObjectTypes = allForObj.QuestionaryObjectTypes.Where(t => t.CompanyId == model.CompanyId).ToList();

            return model;
        }

        public async Task<QuestionaryDto> GetAllForQuestionaryForUserCreate(QuestionaryDto model, string idUser)
        {
            var obj = await GetAllForQuestionaryUser(idUser);
            model.ApplicationCompanies = obj.ApplicationCompanies;
            model.QuestionaryObjectTypes = obj.QuestionaryObjectTypes;
            model.SelectableAnswersLists = obj.SelectableAnswersLists;
            if (model.QuestionaryQuestions != null)
            {
                foreach (QuestionaryQuestions question in model.QuestionaryQuestions)
                {
                    if (question.SelectableAnswersListId != 0)
                    {
                        List<QuestionaryInputFieldTypes> currentInputFields =
                            await _questionaryInputFieldTypesRepository.GetAllCurrent(question.SelectableAnswersListId);
                        question.CurrentQuestionaryInputFieldTypes = currentInputFields;
                        List<SelectableAnswers> current =
                            await _selectableAnswersListRepository.GetSelectableAnswersAsync(question.SelectableAnswersListId);
                        question.CurrentSelectableAnswerses = current;
                    }
                }
            }
            return model;
        }

        public Task<bool> IfQuestionaryCurrentInCompany(int idCompany, int idObjType, int idQuestionary)
        {
            var current =
                _questionaryRepository.IfQuestionaryCurrentInCompanyAsync(idCompany, idObjType, idQuestionary);
            return current;
        }

        public async Task<QuestionaryDto> AnswersGetAll(int id, int index, int qqId)
        {
            QuestionaryDto model = new QuestionaryDto();
            model.QuestionaryInputFieldTypes = await _fieldTypesRepository.GetAllCurrent(id);
            model.IndexCurrentQuestion = index;
            foreach (var _ in Enumerable.Range(0, index + 1))
            {
                model.QuestionaryQuestions.Add( new QuestionaryQuestions());
            }
            model.SelectableAnswers = await _selectableAnswersListRepository.GetSelectableAnswersAsync(id);
            return model;
        }

        public async Task<QuestionaryDto> ObjectTypesGetAll(int id)
        {
            QuestionaryDto model = new QuestionaryDto();
            model.QuestionaryObjectTypes = await _questionaryObjectTypesRepository.GetAllCurrent(id);
            return model;
        }
    }
}