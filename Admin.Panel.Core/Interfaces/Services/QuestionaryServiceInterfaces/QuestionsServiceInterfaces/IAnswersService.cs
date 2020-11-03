using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Questions;

namespace Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces
{
    public interface IAnswersService
    {
        Task<SelectableAnswersLists> GetInputs(SelectableAnswersLists model);
    }
}