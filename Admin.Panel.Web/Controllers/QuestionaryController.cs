using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Entities.Questionary.Questions;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Panel.Web.Controllers
{
    public class QuestionaryController : Controller
    {
        private readonly IQuestionaryService _questionaryService;
        private readonly IQuestionaryRepository _questionaryRepository;

        public QuestionaryController(IQuestionaryService questionaryService, IQuestionaryRepository questionaryRepository)
        {
            _questionaryService = questionaryService;
            _questionaryRepository = questionaryRepository;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, QuestionaryEdit")]
        public async Task<IActionResult> Create()
        {
            QuestionaryDto model = await _questionaryService.GetAllForQuestionary();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator, QuestionaryEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(QuestionaryDto model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryRepository.CreateAsync(model);
                return RedirectToAction("GetAll", "Questionary");
            }
            model = await _questionaryService.GetAllForQuestionary(); 
            return View(model);
        }
        
        [HttpGet]
        [Authorize(Roles = "QuestionaryEdit")]
        public async Task<IActionResult> CreateForUser()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var model = await _questionaryService.GetAllForQuestionaryForUser(userId);
                return View("Create", model);
            }
            catch (Exception)
            {
                return RedirectToAction("", "");
            }
        }
        
        [HttpPost]
        [Authorize(Roles = "QuestionaryEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateForUser(QuestionaryDto model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryRepository.CreateAsync(model);
                return RedirectToAction("GetAllForUser", "ObjectsPropValues");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model = await _questionaryService.GetAllForQuestionaryForUser(userId);
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
    }
}