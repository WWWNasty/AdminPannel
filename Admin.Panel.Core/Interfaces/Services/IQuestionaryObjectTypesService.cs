using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;

namespace Admin.Panel.Core.Interfaces.Services
{
    public interface IQuestionaryObjectTypesService
    {
        public Task<QuestionaryObjectType> GetAllProperties();
        public Task<QuestionaryObjectType> GetObjectForUpdare(int id);
    }
}
