using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;

namespace Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces
{
    public interface IQuestionaryObjectService
    {
        public Task<QuestionaryObject> GetAllForCreate();
        public Task<QuestionaryObject> GetAllForCreateForUser(string userId);
        public Task<QuestionaryObject> GetAllForUpdate(int id);
        public Task<QuestionaryObject> GetAllForUpdateForUser(int id, string userId);
     

    }
}
