using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;

namespace Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces
{
    public interface IQuestionaryObjectService
    {
        Task<QuestionaryObject> GetAllForCreate(QuestionaryObject model);
        Task<QuestionaryObject> GetAllForCreateForUser(QuestionaryObject model, string userId);
        Task<QuestionaryObject> GetAllForUpdate(QuestionaryObject model);
        Task<QuestionaryObject> GetAllForUpdateForUser(QuestionaryObject model, string userId);
     

    }
}
