using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Completed;

namespace Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.Completed
{
    public interface ICompletedQuestionaryRepository
    {
        Task<List<CompletedQuestionary>> GetAllAsync(QueryParameters model);
    }
}