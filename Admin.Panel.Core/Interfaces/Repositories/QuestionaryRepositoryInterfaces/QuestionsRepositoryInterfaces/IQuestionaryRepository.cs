using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Questions;

namespace Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces
{
    public interface IQuestionaryRepository
    {
        public Task<QuestionaryDto> GetAsync(int id);
        public Task<List<QuestionaryDto>> GetAllAsync();
        //public Task<List<SelectableAnswersLists>> GetAllActiveAsync();
        public Task<List<QuestionaryDto>> GetAllForUserAsync(string userId);
        public Task<QuestionaryDto> CreateAsync(QuestionaryDto selectableAnswersList);
        public Task<QuestionaryDto> UpdateAsync(QuestionaryDto questionary);
    }
}