using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;

namespace Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces
{
    public interface IQuestionaryObjectRepository
    {
        public Task<QuestionaryObject> GetAsync(int id);
        public Task<List<QuestionaryObject>> GetAllAsync();
        public Task<List<QuestionaryObject>> GetAllActiveAsync();
        public Task<List<QuestionaryObject>> GetAllForUserAsync(int userId);
        public Task<List<QuestionaryObject>> GetAllActiveForUserAsync(int userId);
        public Task<QuestionaryObject> CreateAsync(QuestionaryObject obj);
        public Task<QuestionaryObject> UpdateAsync(QuestionaryObject obj);
        public Task<List<ObjectPropertyValues>> GetPropertiesForUpdate(int idTypeObj);
        public Task<List<ObjectPropertyValues>> GetPropertiesForCreate(int id);
    }
}
