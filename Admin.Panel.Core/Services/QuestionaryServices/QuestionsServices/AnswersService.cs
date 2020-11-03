using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Questions;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces;

namespace Admin.Panel.Core.Services.QuestionaryServices.QuestionsServices
{
    public class AnswersService : IAnswersService
    {
        private readonly IQuestionaryInputFieldTypesRepository _fieldTypesRepository;

        public AnswersService(IQuestionaryInputFieldTypesRepository fieldTypesRepository)
        {
            _fieldTypesRepository = fieldTypesRepository;
        }

        public async Task<SelectableAnswersLists> GetInputs(SelectableAnswersLists model)
        {
            var inputs = await _fieldTypesRepository.GetAll();
            model.QuestionaryInputFieldTypeses = inputs;
            return model;
        }
    }
}