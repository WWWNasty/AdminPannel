using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Entities.Questionary.Questions;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Admin.Panel.Web.Controllers
{
    public class AnswersController : Controller
    {
        private readonly ISelectableAnswersListRepository _selectableAnswersListRepository;
        private readonly ILogger<AnswersController> _logger;
        private readonly IAnswersService _answersService;

        public AnswersController(
            ISelectableAnswersListRepository selectableAnswersListRepository,
            ILogger<AnswersController> logger, IAnswersService answersService)
        {
            _selectableAnswersListRepository = selectableAnswersListRepository;
            _logger = logger;
            _answersService = answersService;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, AnswerEdit")]
        public async Task<IActionResult> Create()
        {
            SelectableAnswersLists model = new SelectableAnswersLists();
            model = await _answersService.GetInputs(model);
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
            model = await _answersService.GetInputs(model);
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
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<IActionResult> Update(int id)
        {
            SelectableAnswersLists model = await _selectableAnswersListRepository.GetAsync(id);
            model = await _answersService.GetInputs(model);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(SelectableAnswersLists model)
        {
            if (ModelState.IsValid)
            {
                await _selectableAnswersListRepository.UpdateAsync(model);
                return RedirectToAction("GetAll", "Answers");            
            }
            model = await _answersService.GetInputs(model);
            return View(model);
        }
    }
}