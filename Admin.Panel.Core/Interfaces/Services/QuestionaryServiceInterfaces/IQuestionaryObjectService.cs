using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;

namespace Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces
{
    public interface IQuestionaryObjectService
    {
        Task<QuestionaryObject> GetAllForCreate();
        Task<QuestionaryObject> GetAllForCreateForUser(string userId);
        Task<QuestionaryObject> GetAllForUpdate(QuestionaryObject model);
        Task<QuestionaryObject> GetAllForUpdateForUser(QuestionaryObject model, string userId);
     

    }
}
