using System.Security.Claims;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Questions;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Admin.Panel.Web.Controllers
{
    public class QuestionaryController : Controller
    {
        private readonly IQuestionaryService _questionaryService;
        private readonly IQuestionaryRepository _questionaryRepository;
        private readonly IQuestionaryInputFieldTypesRepository _fieldTypesRepository;

        public QuestionaryController(
            IQuestionaryService questionaryService,
            IQuestionaryRepository questionaryRepository,
            IQuestionaryInputFieldTypesRepository fieldTypesRepository
        )
        {
            _questionaryService = questionaryService;
            _questionaryRepository = questionaryRepository;
            _fieldTypesRepository = fieldTypesRepository;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, QuestionaryEdit")]
        public async Task<IActionResult> Create()
        {
            QuestionaryDto model = new QuestionaryDto();
            model = await _questionaryService.GetAllForQuestionaryCreate(model);
            model.QuestionaryQuestions.Add(new QuestionaryQuestions());
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
                return RedirectToAction("GetAll", "Questionary");
            }

            model = await _questionaryService.GetAllForQuestionaryCreate(model);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "QuestionaryEdit")]
        public async Task<IActionResult> CreateForUser()
        {
            QuestionaryDto model = new QuestionaryDto();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model = await _questionaryService.GetAllForQuestionaryForUserCreate(model, userId);
            model.QuestionaryQuestions.Add(new QuestionaryQuestions());
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
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            if (User.IsInRole("SuperAdministrator"))
            {
                var model = await _questionaryRepository.GetAllAsync();
                return View(model);
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var model = await _questionaryRepository.GetAllForUserAsync(userId);
                return View(model);
            }
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
        public async Task<ActionResult> AnswersGetAll(int id, [FromQuery] int index, [FromQuery] int qqId )
        {
            QuestionaryDto model = await _questionaryService.AnswersGetAll(id, index, qqId);
            return PartialView("_SelectableAnswers", model);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> ObjectTypesGetAll(int id)
        {
            QuestionaryDto model = await _questionaryService.ObjectTypesGetAll(id);
            return PartialView("_ObjectTypes", model);
        }
    }
}