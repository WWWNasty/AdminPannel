using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Completed;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.Completed;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.Completed;

namespace Admin.Panel.Core.Services.QuestionaryServices.Completed
{
    public class CompletedQuestionaryService: ICompletedQuestionaryService
    {
        private readonly ICompletedQuestionaryRepository _completedQuestionaryRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IQuestionaryObjectTypesRepository _questionaryObjectTypesRepository;
        private readonly IQuestionaryObjectRepository _questionaryObjectRepository;

        public CompletedQuestionaryService(
            ICompletedQuestionaryRepository completedQuestionaryRepository, 
            ICompanyRepository companyRepository, 
            IQuestionaryObjectTypesRepository questionaryObjectTypesRepository, 
            IQuestionaryObjectRepository questionaryObjectRepository)
        {
            _completedQuestionaryRepository = completedQuestionaryRepository;
            _companyRepository = companyRepository;
            _questionaryObjectTypesRepository = questionaryObjectTypesRepository;
            _questionaryObjectRepository = questionaryObjectRepository;
        }

        public async Task<QueryParameters> GetAll(QueryParameters model)
        {
           model = await _completedQuestionaryRepository.GetAllAsync(model);
           model.ApplicationCompanies = await _companyRepository.GetAllAsync();
           model.QuestionaryObjects = await _questionaryObjectRepository.GetAllAsync();
           model.QuestionaryObjectTypes = await _questionaryObjectTypesRepository.GetAllAsync();
           return model;
        }

        public async Task<QueryParameters> GetAllForUser(QueryParameters model, int userId)
        {
            model = await _completedQuestionaryRepository.GetAllAsync(model);
            model.ApplicationCompanies = await _companyRepository.GetAllForUserAsync(userId);
            model.QuestionaryObjects = await _questionaryObjectRepository.GetAllForUserAsync(userId);
            model.QuestionaryObjectTypes = await _questionaryObjectTypesRepository.GetAllAsync();
            return model;
        }
    }
}