using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Entities.Questionary.Questions;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces;

namespace Admin.Panel.Core.Services.QuestionaryServices.QuestionsServices
{
    public class QuestionaryService: IQuestionaryService
    {
        private readonly IQuestionaryObjectTypesRepository _questionaryObjectTypesRepository;
        private readonly ISelectableAnswersListRepository _selectableAnswersListRepository;
        private readonly IQuestionaryInputFieldTypesRepository _questionaryInputFieldTypesRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IQuestionaryRepository _questionaryRepository;

        public QuestionaryService(
            IQuestionaryObjectTypesRepository questionaryObjectTypesRepository, 
            ISelectableAnswersListRepository selectableAnswersListRepository, 
            IQuestionaryInputFieldTypesRepository questionaryInputFieldTypesRepository, 
            ICompanyRepository companyRepository, IQuestionaryRepository questionaryRepository)
        {
            _questionaryObjectTypesRepository = questionaryObjectTypesRepository;
            _selectableAnswersListRepository = selectableAnswersListRepository;
            _questionaryInputFieldTypesRepository = questionaryInputFieldTypesRepository;
            _companyRepository = companyRepository;
            _questionaryRepository = questionaryRepository;
        }

        private async Task<QuestionaryDto> GetAllForQuestionary()
        {
            List<QuestionaryObjectType> objectTypes = await _questionaryObjectTypesRepository.GetAllActiveAsync();
            List<QuestionaryInputFieldTypes> inputFields = await _questionaryInputFieldTypesRepository.GetAll();
            List<SelectableAnswersLists> answersListTypes = await _selectableAnswersListRepository.GetAllActiveAsync();
            List<ApplicationCompany> companies = await _companyRepository.GetAllActiveAsync();
            QuestionaryDto obj = new QuestionaryDto();
            obj.ApplicationCompanies = companies;
            obj.QuestionaryObjectTypes = objectTypes;
            obj.QuestionaryInputFieldTypes = inputFields;
            obj.SelectableAnswersLists = answersListTypes;
            return obj;
        }
            
        private async Task<QuestionaryDto> GetAllForQuestionaryUser(string idUser)
        {
            List<QuestionaryObjectType> objectTypes = await _questionaryObjectTypesRepository.GetAllActiveAsync();
            List<QuestionaryInputFieldTypes> inputFields = await _questionaryInputFieldTypesRepository.GetAll();
            List<SelectableAnswersLists> answersListTypes = await _selectableAnswersListRepository.GetAllActiveAsync();
            List<ApplicationCompany> companies = await _companyRepository.GetAllActiveForUserAsync(idUser);
            QuestionaryDto obj = new QuestionaryDto();
            obj.ApplicationCompanies = companies;
            obj.QuestionaryObjectTypes = objectTypes;
            obj.QuestionaryInputFieldTypes = inputFields;
            obj.SelectableAnswersLists = answersListTypes;
            return obj;
        }
        
        public async Task<QuestionaryDto> GetAllForQuestionaryUpdate(int idQuestionary)
        {
            QuestionaryDto obj = new QuestionaryDto();
            var allForObj = await GetAllForQuestionary();
            if (idQuestionary != null)
            {
                obj = await _questionaryRepository.GetAsync(idQuestionary);
            }

            obj.ApplicationCompanies = allForObj.ApplicationCompanies;
            obj.QuestionaryObjectTypes = allForObj.QuestionaryObjectTypes;
            obj.SelectableAnswersLists = allForObj.SelectableAnswersLists;
            foreach (QuestionaryQuestions question in obj.QuestionaryQuestions)
            {
                List<QuestionaryInputFieldTypes> currentInputFields = await _questionaryInputFieldTypesRepository.GetAllCurrent(question.SelectableAnswersListId);
                question.CurrentQuestionaryInputFieldTypes = currentInputFields;
            }
            return obj;
        }

        public async Task<QuestionaryDto> GetAllForQuestionaryCreate()
        {
            
            var obj = await GetAllForQuestionary();
            QuestionaryDto result = new QuestionaryDto();
            result.ApplicationCompanies = obj.ApplicationCompanies;
            result.QuestionaryObjectTypes = obj.QuestionaryObjectTypes;
            result.SelectableAnswersLists = obj.SelectableAnswersLists;
            return result;
        }
        
        public async Task<QuestionaryDto> GetAllForQuestionaryForUserUpdate(string idUser, int idQuestionary)
        {
           
            QuestionaryDto obj = new QuestionaryDto();
            var allForObj = await GetAllForQuestionaryUser(idUser);
            if (idQuestionary != null)
            {
                obj = await _questionaryRepository.GetAsync(idQuestionary);
            }
            obj.ApplicationCompanies = allForObj.ApplicationCompanies;
            obj.QuestionaryObjectTypes = allForObj.QuestionaryObjectTypes;
            obj.SelectableAnswersLists = allForObj.SelectableAnswersLists;
            foreach (QuestionaryQuestions question in obj.QuestionaryQuestions)
            {
                List<QuestionaryInputFieldTypes> currentInputFields = await _questionaryInputFieldTypesRepository.GetAllCurrent(question.Id);
                question.CurrentQuestionaryInputFieldTypes = currentInputFields;
            }
            return obj;
        }
        public async Task<QuestionaryDto> GetAllForQuestionaryForUserCreate(string idUser)
        {
            var obj = await GetAllForQuestionaryUser(idUser);
            QuestionaryDto result = new QuestionaryDto();
            result.ApplicationCompanies = obj.ApplicationCompanies;
            result.QuestionaryObjectTypes = obj.QuestionaryObjectTypes;
            result.SelectableAnswersLists = obj.SelectableAnswersLists;
            return result;
        }
  
        public Task<bool> IfQuestionaryCurrentInCompany(int idCompany, int idObjType)
        {
            var current = _questionaryRepository.IfQuestionaryCurrentInCompanyAsync(idCompany, idObjType);
            return current;
        }

      
        
    }
}