using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Completed;

namespace Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.Completed
{
    public interface ICompletedQuestionaryService
    {
        Task<QueryParameters> GetAll(QueryParameters model);
    }
}