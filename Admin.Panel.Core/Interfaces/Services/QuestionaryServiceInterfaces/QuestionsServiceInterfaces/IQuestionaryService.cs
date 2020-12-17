using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Questions;

namespace Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces
{
    public interface IQuestionaryService
    {
        Task<QuestionaryDto> GetAllForQuestionaryCreate(QuestionaryDto model);
        Task<QuestionaryDto> GetAllForQuestionaryUpdate(QuestionaryDto model);
        Task<QuestionaryDto> GetAllForQuestionaryForUserCreate(QuestionaryDto model, string idUser);
        Task<QuestionaryDto> GetAllForQuestionaryForUserUpdate(QuestionaryDto model, string idUser);
        Task<bool> IfQuestionaryCurrentInCompany(int idCompany, int idObjType, int idQuestionary);
        Task<QuestionaryDto> AnswersGetAll(int id, int index, int qqId);
        Task<QuestionaryDto> ObjectTypesGetAll(int id);
    }
}