using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces;

namespace Admin.Panel.Core.Services.QuestionaryServices
{
    public class QuestionaryObjectTypesService : IQuestionaryObjectTypesService
    {
        private readonly IObjectPropertiesRepository _objectPropertiesRepository;
        private readonly IQuestionaryObjectTypesRepository _questionaryObjectTypesRepository;

        public QuestionaryObjectTypesService(IObjectPropertiesRepository objectPropertiesRepository,
            IQuestionaryObjectTypesRepository questionaryObjectTypesRepository)
        {
            _objectPropertiesRepository = objectPropertiesRepository;
            _questionaryObjectTypesRepository = questionaryObjectTypesRepository;
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
            //List<ObjectProperty> allProperties = await _objectPropertiesRepository.GetAllActiveAsync();
            var obj = await _questionaryObjectTypesRepository.GetAsync(id);
            //obj.ObjectProperties = allProperties;
            return obj;
        }
    }
}