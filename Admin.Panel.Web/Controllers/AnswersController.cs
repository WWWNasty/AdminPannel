using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Entities.Questionary.Questions;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Panel.Web.Controllers
{
    public class AnswersController : Controller
    {
        private readonly ISelectableAnswersListRepository _selectableAnswersListRepository;
        private readonly IQuestionaryInputFieldTypesRepository _fieldTypesRepository;

        public AnswersController(ISelectableAnswersListRepository selectableAnswersListRepository, IQuestionaryInputFieldTypesRepository fieldTypesRepository)
        {
            _selectableAnswersListRepository = selectableAnswersListRepository;
            _fieldTypesRepository = fieldTypesRepository;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, AnswerEdit")]
        public async Task<IActionResult> Create()
        {
            SelectableAnswersLists model = new SelectableAnswersLists();
            var inputs = await _fieldTypesRepository.GetAll();
            model.QuestionaryInputFieldTypeses = inputs;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator, AnswerEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SelectableAnswersLists model)
        {
            if (ModelState.IsValid)
            {
                await _selectableAnswersListRepository.CreateAsync(model);
                return RedirectToAction("GetAll", "Answers");
            }
            var inputs = await _fieldTypesRepository.GetAll();
            model.QuestionaryInputFieldTypeses = inputs;
            return View(model);
        }
        
        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, AnswerRead")]
        public async Task<ActionResult> Get(int id)
        {
            var model = await _selectableAnswersListRepository.GetAsync(id);
            return View(model);
        }
        
        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, AnswerRead")]
        public async Task<ActionResult> GetAll()
        {
            var model = await _selectableAnswersListRepository.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, TypesObjectEdit")]
        public async Task<IActionResult> Update(int id)
        {
            var inputs = await _fieldTypesRepository.GetAll();
            SelectableAnswersLists model = await _selectableAnswersListRepository.GetAsync(id);
            model.QuestionaryInputFieldTypeses = inputs;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator, TypesObjectEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(SelectableAnswersLists model)
        {
            if (ModelState.IsValid)
            {
                await _selectableAnswersListRepository.UpdateAsync(model);
                return RedirectToAction("GetAll", "Answers");            
            }
            model = await _selectableAnswersListRepository.GetAsync(model.Id);
            var inputs = await _fieldTypesRepository.GetAll();
            model.QuestionaryInputFieldTypeses = inputs;
            return View(model);
        }
    }
}