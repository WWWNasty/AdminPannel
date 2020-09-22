using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Questions;

namespace Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces
{
    public interface IQuestionaryService
    {
        public Task<QuestionaryDto> GetAllForQuestionary();
        public Task<QuestionaryDto> GetAllForQuestionaryForUser(string idUser);
    }
}