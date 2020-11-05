using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Questions;
using Admin.Panel.Core.Entities.UserManage;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Admin.Panel.Web.Controllers
{
    public class QuestionaryController : Controller
    {
        private readonly IQuestionaryService _questionaryService;
        private readonly IQuestionaryRepository _questionaryRepository;
        private readonly ISelectableAnswersListRepository _selectableAnswersListRepository;
        private readonly IQuestionaryInputFieldTypesRepository _fieldTypesRepository;
        private readonly IQuestionaryObjectTypesRepository _questionaryObjectTypesRepository;
        private readonly IQuestionaryInputFieldTypesRepository _questionaryInputFieldTypesRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<QuestionaryController> _logger;
        private readonly UserManager<User> _userManager;
        
        public QuestionaryController(
            IQuestionaryService questionaryService,
            IQuestionaryRepository questionaryRepository,
            ISelectableAnswersListRepository selectableAnswersListRepository,
            IQuestionaryInputFieldTypesRepository fieldTypesRepository,
            IQuestionaryObjectTypesRepository questionaryObjectTypesRepository,
            IQuestionaryInputFieldTypesRepository questionaryInputFieldTypesRepository,
            ICompanyRepository companyRepository,
            ILogger<QuestionaryController> logger, UserManager<User> userManager)
        {
            _questionaryService = questionaryService;
            _questionaryRepository = questionaryRepository;
            _selectableAnswersListRepository = selectableAnswersListRepository;
            _fieldTypesRepository = fieldTypesRepository;
            _questionaryObjectTypesRepository = questionaryObjectTypesRepository;
            _questionaryInputFieldTypesRepository = questionaryInputFieldTypesRepository;
            _companyRepository = companyRepository;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, QuestionaryEdit")]
        public async Task<IActionResult> Create()
        {
            QuestionaryDto mod = new QuestionaryDto();
            QuestionaryDto model = await _questionaryService.GetAllForQuestionaryCreate(mod);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator, QuestionaryEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(QuestionaryDto model)
        {
            if (ModelState.IsValid)
            {
                var current = await _questionaryService.IfQuestionaryCurrentInCompany(model.CompanyId,
                    model.ObjectTypeId,
                    model.Id);
                if (current)
                {
                    model = await _questionaryService.GetAllForQuestionaryCreate(model);
                    model.IfQuestionaryCurrentInCompany = true;
                    return View(model);
                }

                await _questionaryRepository.CreateAsync(model);
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _logger.LogInformation("Анкета {0} успешно создана для компании с Id: {1} пользователем с Id {2}.",
                    model.Name,
                    model.CompanyId, userId);
                return RedirectToAction("GetAll", "Questionary");
            }

            model = await _questionaryService.GetAllForQuestionaryCreate(model);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "QuestionaryEdit")]
        public async Task<IActionResult> CreateForUser()
        {
            QuestionaryDto mod = new QuestionaryDto();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await _questionaryService.GetAllForQuestionaryForUserCreate(mod, userId);
            return View("Create", model);
        }

        [HttpPost]
        [Authorize(Roles = "QuestionaryEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateForUser(QuestionaryDto model)
        {
            if (ModelState.IsValid)
            {
                var current =
                    await _questionaryService.IfQuestionaryCurrentInCompany(model.CompanyId, model.ObjectTypeId,
                        model.Id);
                if (current)
                {
                    var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    model = await _questionaryService.GetAllForQuestionaryForUserCreate(model, user);
                    model.IfQuestionaryCurrentInCompany = true;
                    return View("Create", model);
                }

                await _questionaryRepository.CreateAsync(model);
                _logger.LogInformation("Анкета {0} успешно создана для компании с Id: {1}.", model.Name,
                    model.CompanyId);
                return RedirectToAction("GetAllForUser", "Questionary");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model = await _questionaryService.GetAllForQuestionaryForUserCreate(model, userId);
            return View("Create", model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, QuestionaryRead")]
        public async Task<ActionResult> Get(int id)
        {
            var model = await _questionaryRepository.GetAsync(id);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<ActionResult> GetAll()
        {
            var model = await _questionaryRepository.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, QuestionaryRead")]
        public async Task<ActionResult> GetAllForUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await _questionaryRepository.GetAllForUserAsync(userId);
            return View("GetAll", model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<ActionResult> Update(int id)
        {
            var model = await _questionaryRepository.GetAsync(id);
            model = await _questionaryService.GetAllForQuestionaryUpdate(model);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(QuestionaryDto model)
        {
            if (ModelState.IsValid)
            {
                var current =
                    await _questionaryService.IfQuestionaryCurrentInCompany(model.CompanyId, model.ObjectTypeId,
                        model.Id);
                if (current && model.IsUsed)
                {
                    model = await _questionaryService.GetAllForQuestionaryUpdate(model);
                    model.IfQuestionaryCurrentInCompany = true;
                    return View("Update", model);
                }

                await _questionaryRepository.UpdateAsync(model);
                _logger.LogInformation("Анкета с Id {0} успешно отредактирована.", model.Id);
                return RedirectToAction("GetAll", "Questionary");
            }

            model = await _questionaryService.GetAllForQuestionaryUpdate(model);
            return View("Update", model);
        }

        [HttpPost]
        [Authorize(Roles = "QuestionaryEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateForUser(QuestionaryDto model)
        {
            if (ModelState.IsValid)
            {
                var current =
                    await _questionaryService.IfQuestionaryCurrentInCompany(model.CompanyId, model.ObjectTypeId,
                        model.Id);
                if (current && model.IsUsed)
                {
                    var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    model = await _questionaryService.GetAllForQuestionaryForUserUpdate(model, user);
                    model.IfQuestionaryCurrentInCompany = true;
                    return View("Update", model);
                }

                await _questionaryRepository.UpdateAsync(model);
                _logger.LogInformation("Анкета с Id {0} успешно отредактирована.", model.Id);
                return RedirectToAction("GetAllForUser", "Questionary");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model = await _questionaryService.GetAllForQuestionaryForUserUpdate(model, userId);
            return View("Update", model);
        }

        [HttpGet]
        [Authorize(Roles = "QuestionaryEdit")]
        public async Task<ActionResult> UpdateForUser(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await _questionaryRepository.GetAsync(id);
            model = await _questionaryService.GetAllForQuestionaryForUserUpdate(model, userId);
            return View("Update", model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> AnswersGetAll(int id, [FromQuery] int index)
        {
            //TODO доставать каждому вопросу варианты ответов
            // SelectableAnswers[] answrs = await _selectableAnswersListRepository.GetSelectableAnswersAsync(id);
            var inputTypes = await _fieldTypesRepository.GetAllCurrent(id);
            QuestionaryDto model = new QuestionaryDto();
            //model.QuestionaryQuestions[i].SelectableAnswers = answrs;
            model.QuestionaryInputFieldTypes = inputTypes;
            model.IndexCurrentQuestion = index;
            return PartialView("_SelectableAnswers", model);
        }
    }
}