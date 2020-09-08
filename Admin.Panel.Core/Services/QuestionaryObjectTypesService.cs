using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.Questionary;
using Admin.Panel.Core.Interfaces.Services;

namespace Admin.Panel.Core.Services
{
    public class QuestionaryObjectTypesService: IQuestionaryObjectTypesService
    {
        private readonly IObjectPropertiesRepository _objectPropertiesRepository;
        private readonly IQuestionaryObjectTypesRepository _questionaryObjectTypesRepository;

        public QuestionaryObjectTypesService(IObjectPropertiesRepository objectPropertiesRepository, IQuestionaryObjectTypesRepository questionaryObjectTypesRepository)
        {
            _objectPropertiesRepository = objectPropertiesRepository;
            _questionaryObjectTypesRepository = questionaryObjectTypesRepository;
        }

        public async Task<QuestionaryObjectType> GetAllProperties()
        {
            List<ObjectProperty> properties = await _objectPropertiesRepository.GetAllAsync();
            QuestionaryObjectType createProperties = new QuestionaryObjectType
            {
                ObjectProperties = properties
            };

            return createProperties;
        }

        public async Task<QuestionaryObjectType> GetObjectForUpdare(int id)
        {
            List<ObjectProperty> properties = await _objectPropertiesRepository.GetAllAsync();
            var obj = await _questionaryObjectTypesRepository.GetAsync(id);
            obj.ObjectProperties = properties;

            return obj;
        }
    }
}
