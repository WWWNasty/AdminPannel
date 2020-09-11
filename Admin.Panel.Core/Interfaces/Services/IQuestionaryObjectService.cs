using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;

namespace Admin.Panel.Core.Interfaces.Services
{
    public interface IQuestionaryObjectService
    {
        public Task<QuestionaryObject> GetAllForCreate();
        public Task<QuestionaryObject> GetAllForUpdate(int id);
        public Task<List<ObjectPropertyValues>> GetPropertiesForUpdate(int id);
        public Task<List<ObjectPropertyValues>> GetPropertiesForCreate();

    }
}
