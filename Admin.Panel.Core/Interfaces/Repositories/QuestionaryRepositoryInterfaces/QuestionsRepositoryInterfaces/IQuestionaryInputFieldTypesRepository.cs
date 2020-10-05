using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Questions;

namespace Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces
{
    public interface IQuestionaryInputFieldTypesRepository
    {
        public Task<List<QuestionaryInputFieldTypes>> GetAll();
        
        public Task<List<QuestionaryInputFieldTypes>> GetAllCurrent(int id);
    }
}