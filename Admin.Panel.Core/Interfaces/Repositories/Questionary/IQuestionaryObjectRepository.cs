using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.Questionary;

namespace Admin.Panel.Core.Interfaces.Repositories.Questionary
{
    public interface IQuestionaryObjectRepository
    {
        public Task<QuestionaryObject> GetAsync(int id);
        public Task<List<QuestionaryObject>> GetAllAsync();
        public Task<QuestionaryObject> CreateAsync(QuestionaryObject obj);
        public Task<QuestionaryObject> UpdateAsync(QuestionaryObject obj);
    }
}
