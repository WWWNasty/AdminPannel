using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;

namespace Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces
{
    public interface IQuestionaryObjectTypesRepository
    {
        public Task<QuestionaryObjectType> GetAsync(int id);
        public Task<List<QuestionaryObjectType>> GetAllAsync();
        public Task<List<QuestionaryObjectType>> GetAllForUserAsync(int userId);
        public Task<List<QuestionaryObjectType>> GetAllActiveAsync();
        public Task<List<QuestionaryObjectType>> GetAllActiveWithoutQuestionaryAsync(int objectTypeId);
        public Task<List<QuestionaryObjectType>> GetAllActiveForUserAsync(int userId);
        public Task<QuestionaryObjectType> CreateAsync(QuestionaryObjectType obj);
        public Task<QuestionaryObjectType> UpdateAsync(QuestionaryObjectType obj);
        public Task<List<QuestionaryObjectType>> GetAllCurrent(int id);
    }
}
