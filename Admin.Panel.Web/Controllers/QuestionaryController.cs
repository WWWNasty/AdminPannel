using System;
using System.Collections.Generic;
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
        private readonly ISelectableAnswersListRepository _selectableAnswersListRepository;

        public QuestionaryController(IQuestionaryService questionaryService, IQuestionaryRepository questionaryRepository, ISelectableAnswersListRepository selectableAnswersListRepository)
        {
            _questionaryService = questionaryService;
            _questionaryRepository = questionaryRepository;
            _selectableAnswersListRepository = selectableAnswersListRepository;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, QuestionaryEdit")]
        public async Task<IActionResult> Create()
        {
            QuestionaryDto model = await _questionaryService.GetAllForQuestionaryCreate();

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
            model = await _questionaryService.GetAllForQuestionaryCreate(); 
            return View(model);
        }
        
        [HttpGet]
        [Authorize(Roles = "QuestionaryEdit")]
        public async Task<IActionResult> CreateForUser()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var model = await _questionaryService.GetAllForQuestionaryForUserCreate(userId);
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
                return RedirectToAction("GetAllForUser", "Questionary");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model = await _questionaryService.GetAllForQuestionaryForUserCreate(userId);
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
            var model = await _questionaryService.GetAllForQuestionaryUpdate(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(QuestionaryDto model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryRepository.UpdateAsync(model);
                return RedirectToAction("GetAll", "Questionary");
            }
            model = await _questionaryService.GetAllForQuestionaryUpdate(model.Id);
            
            return View("Update", model);
        }
        
        [HttpPost]
        [Authorize(Roles = "QuestionaryEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateForUser(QuestionaryDto model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryRepository.UpdateAsync(model);
                return RedirectToAction("GetAllForUser", "Questionary");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            model = await _questionaryService.GetAllForQuestionaryForUserUpdate(userId, model.Id);
            
            return View("Update", model);
        }

        [HttpGet]
        [Authorize(Roles = "QuestionaryEdit")]
        public async Task<ActionResult> UpdateForUser(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await _questionaryService.GetAllForQuestionaryForUserUpdate(userId, id);
            return View("Update", model);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> AnswersGetAll(int id)
        {
            //TODO доставать каждому вопросу варианты ответов
           // SelectableAnswers[] answrs = await _selectableAnswersListRepository.GetSelectableAnswersAsync(id);
            QuestionaryDto model = new QuestionaryDto();
            //model.QuestionaryQuestions[i].SelectableAnswers = answrs;
            return PartialView("_SelectableAnswers", model);
        }
    }
}