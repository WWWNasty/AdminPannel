using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Questions;

namespace Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces
{
    public interface IQuestionaryService
    {
        public Task<QuestionaryDto> GetAllForQuestionaryCreate(QuestionaryDto model);
        public Task<QuestionaryDto> GetAllForQuestionaryUpdate(QuestionaryDto model);
        public Task<QuestionaryDto> GetAllForQuestionaryForUserCreate(QuestionaryDto model, string idUser);
        public Task<QuestionaryDto> GetAllForQuestionaryForUserUpdate(QuestionaryDto model, string idUser);
        public Task<bool> IfQuestionaryCurrentInCompany(int idCompany, int idObjType, int idQuestionary);
    }
}