using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;

namespace Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces
{
    public interface IPropertyService
    {
        Task<List<ObjectPropertyValues>> GetPropertiesForUpdateObject();
    }
}