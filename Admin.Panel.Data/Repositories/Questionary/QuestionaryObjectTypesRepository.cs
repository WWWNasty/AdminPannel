using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.Questionary;

namespace Admin.Panel.Data.Repositories.Questionary
{
    public class QuestionaryObjectTypesRepository: IQuestionaryObjectTypesRepository
    {
        public Task<QuestionaryObjectType> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<QuestionaryObjectType>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<QuestionaryObjectType> CreateAsync(QuestionaryObjectType obj)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionaryObjectType> UpdateAsync(QuestionaryObjectType obj)
        {
            throw new NotImplementedException();
        }
    }
}
