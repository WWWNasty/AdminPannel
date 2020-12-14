using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces;

namespace Admin.Panel.Core.Services.QuestionaryServices
{
    public class QuestionaryObjectTypesService : IQuestionaryObjectTypesService
    {
        private readonly IQuestionaryObjectTypesRepository _questionaryObjectTypesRepository;
        private readonly ICompanyRepository _companyRepository;

        public QuestionaryObjectTypesService(
            IQuestionaryObjectTypesRepository questionaryObjectTypesRepository, 
            ICompanyRepository companyRepository)
        {
            _questionaryObjectTypesRepository = questionaryObjectTypesRepository;
            _companyRepository = companyRepository;
        }

        // public async Task<QuestionaryObjectType> GetAllProperties()
        // {
        //     //List<ObjectProperty> properties = await _objectPropertiesRepository.GetAllActiveAsync();
        //     QuestionaryObjectType createProperties = new QuestionaryObjectType
        //     {
        //         ObjectProperties = properties
        //     };
        //     return createProperties;
        // }

        public async Task<QuestionaryObjectType> GetObjectForUpdare(int id)
        {
            var obj = await _questionaryObjectTypesRepository.GetAsync(id);
            obj.Companies = await _companyRepository.GetAllActiveAsync();
            return obj;
        }

        public async Task<QuestionaryObjectType> GetObjectForUpdareForUser(int id, string userId)
        {
            var obj = await _questionaryObjectTypesRepository.GetAsync(id);
            obj.Companies = await _companyRepository.GetAllActiveForUserAsync(userId);
            return obj;
        }
    }
}