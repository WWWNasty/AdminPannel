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

        public async Task<QuestionaryDto> GetAllForQuestionaryUpdate(int idQuestionary)
        {
            QuestionaryDto obj = new QuestionaryDto();
            if (idQuestionary != null)
            {
                obj = await _questionaryRepository.GetAsync(idQuestionary);
            }
            List<QuestionaryObjectType> objectTypes = await _questionaryObjectTypesRepository.GetAllActiveAsync();
            List<QuestionaryInputFieldTypes> inputFields = await _questionaryInputFieldTypesRepository.GetAll();
            List<SelectableAnswersLists> answersListTypes = await _selectableAnswersListRepository.GetAllActiveAsync();
            List<ApplicationCompany> companies = await _companyRepository.GetAllActiveAsync();
            
            obj.ApplicationCompanies = companies;
            obj.QuestionaryObjectTypes = objectTypes;
            obj.QuestionaryInputFieldTypes = inputFields;
            obj.SelectableAnswersLists = answersListTypes;
            return obj;
        }

        public async Task<QuestionaryDto> GetAllForQuestionaryCreate()
        {
            QuestionaryDto obj = new QuestionaryDto();
            List<QuestionaryObjectType> objectTypes = await _questionaryObjectTypesRepository.GetAllActiveAsync();
            List<QuestionaryInputFieldTypes> inputFields = await _questionaryInputFieldTypesRepository.GetAll();
            List<SelectableAnswersLists> answersListTypes = await _selectableAnswersListRepository.GetAllActiveAsync();
            List<ApplicationCompany> companies = await _companyRepository.GetAllActiveAsync();
            
            obj.ApplicationCompanies = companies;
            obj.QuestionaryObjectTypes = objectTypes;
            obj.QuestionaryInputFieldTypes = inputFields;
            obj.SelectableAnswersLists = answersListTypes;
            return obj;
        }
        
        public async Task<QuestionaryDto> GetAllForQuestionaryForUserUpdate(string idUser, int idQuestionary)
        {
            QuestionaryDto obj = new QuestionaryDto();
            if (idQuestionary != null)
            {
                obj = await _questionaryRepository.GetAsync(idQuestionary);
            } 
            
            List<QuestionaryObjectType> objectTypes = await _questionaryObjectTypesRepository.GetAllActiveAsync();
            List<QuestionaryInputFieldTypes> inputFields = await _questionaryInputFieldTypesRepository.GetAll();
            List<SelectableAnswersLists> answersListTypes = await _selectableAnswersListRepository.GetAllActiveAsync();
            List<ApplicationCompany> companies = await _companyRepository.GetAllActiveForUserAsync(idUser);
           
            obj.ApplicationCompanies = companies;
            obj.QuestionaryObjectTypes = objectTypes;
            obj.QuestionaryInputFieldTypes = inputFields;
            obj.SelectableAnswersLists = answersListTypes;
            return obj;
        }
        
        public async Task<QuestionaryDto> GetAllForQuestionaryForUserCreate(string idUser)
        {
            QuestionaryDto obj = new QuestionaryDto();
            List<QuestionaryObjectType> objectTypes = await _questionaryObjectTypesRepository.GetAllActiveAsync();
            List<QuestionaryInputFieldTypes> inputFields = await _questionaryInputFieldTypesRepository.GetAll();
            List<SelectableAnswersLists> answersListTypes = await _selectableAnswersListRepository.GetAllActiveAsync();
            List<ApplicationCompany> companies = await _companyRepository.GetAllActiveForUserAsync(idUser);
           
            obj.ApplicationCompanies = companies;
            obj.QuestionaryObjectTypes = objectTypes;
            obj.QuestionaryInputFieldTypes = inputFields;
            obj.SelectableAnswersLists = answersListTypes;
            return obj;
        }
    }
}