using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;

namespace Admin.Panel.Core.Interfaces.Repositories.Questionary
{
    public interface IQuestionaryObjectTypesRepository
    {
        public Task<QuestionaryObjectType> GetAsync(int id);
        public Task<List<QuestionaryObjectType>> GetAllAsync();
        public Task<List<QuestionaryObjectType>> GetAllActiveAsync();
        public Task<QuestionaryObjectType> CreateAsync(QuestionaryObjectType obj);
        public Task<QuestionaryObjectType> UpdateAsync(QuestionaryObjectType obj);
    }
}
