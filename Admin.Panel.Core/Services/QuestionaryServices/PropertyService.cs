using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces;

namespace Admin.Panel.Core.Services.QuestionaryServices
{
    public class PropertyService: IPropertyService
    {
        private readonly IObjectPropertiesRepository _objectPropertiesRepository;

        public PropertyService(IObjectPropertiesRepository objectPropertiesRepository)
        {
            _objectPropertiesRepository = objectPropertiesRepository;
        }


        public async Task<List<ObjectPropertyValues>> GetPropertiesForUpdateObject()
        {
           var prop =  await _objectPropertiesRepository.GetAllAsync();
           return null;
        }
    }
}