using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.Questionary.Questions;

namespace Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces
{
    public interface ISelectableAnswersListRepository
    {
        public Task<SelectableAnswersLists> GetAsync(int id);
        public Task <SelectableAnswers[]> GetSelectableAnswersAsync(int id);
        public Task<List<SelectableAnswersLists>> GetAllAsync();
        public Task<List<SelectableAnswersLists>> GetAllActiveAsync();
        //public Task<List<ApplicationCompany>> GetAllActiveForUserAsync(string userId);
        public Task<SelectableAnswersLists> CreateAsync(SelectableAnswersLists selectableAnswersList);
        public Task<SelectableAnswersLists> UpdateAsync(SelectableAnswersLists answersLists);
    }
}