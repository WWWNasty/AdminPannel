using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Questions;

namespace Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces
{
    public interface IQuestionaryService
    {
        public Task<QuestionaryDto> GetAllForQuestionaryCreate();
        public Task<QuestionaryDto> GetAllForQuestionaryUpdate(int idQuestionary);
        public Task<QuestionaryDto> GetAllForQuestionaryForUserCreate(string idUser);
        public Task<QuestionaryDto> GetAllForQuestionaryForUserUpdate(string idUser, int idQuestionary);
        public Task<bool> IfQuestionaryCurrentInCompany(int idCompany, int idObjType);
    }
}