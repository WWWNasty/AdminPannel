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

        public AnswersController(ISelectableAnswersListRepository selectableAnswersListRepository)
        {
            _selectableAnswersListRepository = selectableAnswersListRepository;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, AnswerEdit")]
        public async Task<IActionResult> Create()
        {
            return View();
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

    }
}