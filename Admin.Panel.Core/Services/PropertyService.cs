using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.Questionary;
using Admin.Panel.Core.Interfaces.Services;

namespace Admin.Panel.Core.Services
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