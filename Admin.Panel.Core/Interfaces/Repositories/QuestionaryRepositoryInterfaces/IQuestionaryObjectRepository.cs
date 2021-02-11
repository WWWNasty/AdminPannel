using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;

namespace Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces
{
    public interface IQuestionaryObjectRepository
    {
        Task<QuestionaryObject> GetAsync(int id);
        Task<List<QuestionaryObject>> GetAllAsync();
        Task<List<QuestionaryObject>> GetAllActiveAsync();
        Task<List<QuestionaryObject>> GetAllForUserAsync(int userId);
        Task<List<QuestionaryObject>> GetAllActiveForUserAsync(int userId);
        Task<QuestionaryObject> CreateAsync(QuestionaryObject obj);
        Task<QuestionaryObject> UpdateAsync(QuestionaryObject obj);
        Task<List<ObjectPropertyValues>> GetPropertiesForUpdate(int idTypeObj);
        Task<bool> IsCodeUnique(QuestionaryObject model);
        Task<bool> IsCodeUniqueObjectInQuestionary(int idObject, string code);

    }
}
