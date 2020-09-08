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

        public QuestionaryObjectTypesService(IObjectPropertiesRepository objectPropertiesRepository)
        {
            _objectPropertiesRepository = objectPropertiesRepository;
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
    }
}
