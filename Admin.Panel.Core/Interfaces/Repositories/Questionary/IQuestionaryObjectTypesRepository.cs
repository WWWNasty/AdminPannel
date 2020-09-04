using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Entities.Questionary.Dtos;

namespace Admin.Panel.Core.Interfaces.Repositories.Questionary
{
    public interface IQuestionaryObjectTypesRepository
    {
        public Task<QuestionaryObjectType> GetAsync(int id);
        public Task<List<QuestionaryObjectType>> GetAllAsync();
        public Task<QuestionaryObjectTypeCreateDto> CreateAsync(QuestionaryObjectTypeCreateDto obj);
        public Task<QuestionaryObjectType> UpdateAsync(QuestionaryObjectType obj);
    }
}
