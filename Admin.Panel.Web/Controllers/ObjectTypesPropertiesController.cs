using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Admin.Panel.Web.Controllers
{
    public class ObjectTypesPropertiesController : Controller
    {
        private readonly IQuestionaryObjectTypesRepository _questionaryObjectTypesRepository;
        private readonly IQuestionaryObjectTypesService _questionaryObjectTypesService;
        private readonly ILogger<QuestionaryController> _logger;
        private readonly ICompanyRepository _companyRepository;


        public ObjectTypesPropertiesController(
            IQuestionaryObjectTypesRepository questionaryObjectTypesRepository,
            IQuestionaryObjectTypesService questionaryObjectTypesService,
            ILogger<QuestionaryController> logger, 
            ICompanyRepository companyRepository)
        { 
            _questionaryObjectTypesRepository = questionaryObjectTypesRepository;
            _questionaryObjectTypesService = questionaryObjectTypesService;
            _logger = logger;
            _companyRepository = companyRepository;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<ActionResult> GetAll()
        {
            List<QuestionaryObjectType> model = await _questionaryObjectTypesRepository.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "TypesObjectRead")]
        public async Task<ActionResult> GetAllForUser()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<QuestionaryObjectType> model = await _questionaryObjectTypesRepository.GetAllForUserAsync(userId);
            return View("GetAll", model);
        }
        
        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, TypesObjectRead")]
        public async Task<ActionResult> Get(int id)
        {
            QuestionaryObjectType model = await _questionaryObjectTypesRepository.GetAsync(id);
            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, TypesObjectEdit")]
        public async Task<IActionResult> Create()
        {
            //QuestionaryObjectType model = await _questionaryObjectTypesService.GetAllProperties();
            QuestionaryObjectType model = new QuestionaryObjectType();
            model.Companies = await _companyRepository.GetAllActiveAsync();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator, TypesObjectEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(QuestionaryObjectType model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryObjectTypesRepository.CreateAsync(model);
                _logger.LogInformation("Тип объекта успешно создан: {0}.", model.Name);
                return RedirectToAction("GetAll", "ObjectTypesProperties");
            }
            model.Companies = await _companyRepository.GetAllActiveAsync();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "TypesObjectEdit")]
        public async Task<IActionResult> CreateForUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            QuestionaryObjectType model = new QuestionaryObjectType();
            model.Companies = await _companyRepository.GetAllActiveForUserAsync(userId);
            return View("Create", model);
        }

        [HttpPost]
        [Authorize(Roles = "TypesObjectEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateForUser(QuestionaryObjectType model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                await _questionaryObjectTypesRepository.CreateAsync(model);
                _logger.LogInformation("Тип объекта успешно создан: {0}.", model.Name);
                return RedirectToAction("GetAllForUser", "ObjectTypesProperties");
            }
            model.Companies = await _companyRepository.GetAllActiveForUserAsync(userId);
            return View("Create", model);
        }
        
        [HttpGet]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<IActionResult> Update(int id)
        {
            QuestionaryObjectType model = await _questionaryObjectTypesService.GetObjectForUpdare(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(QuestionaryObjectType model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryObjectTypesRepository.UpdateAsync(model);
                _logger.LogInformation("Тип объекта с Id: {0} успешно отредактирован.", model.Id);
                return RedirectToAction("GetAll", "ObjectTypesProperties");
            }

            model = await _questionaryObjectTypesService.GetObjectForUpdare(model.Id);
            return View(model);
        }
        
        [HttpGet]
        [Authorize(Roles = "TypesObjectEdit")]
        public async Task<IActionResult> UpdateForUser(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            QuestionaryObjectType model = await _questionaryObjectTypesService.GetObjectForUpdareForUser(id, userId);
            return View("Update", model);
        }

        [HttpPost]
        [Authorize(Roles = "TypesObjectEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateForUser(QuestionaryObjectType model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                await _questionaryObjectTypesRepository.UpdateAsync(model);
                _logger.LogInformation("Тип объекта с Id: {0} успешно отредактирован.", model.Id);
                return RedirectToAction("GetAllForUser", "ObjectTypesProperties");
            }

            model = await _questionaryObjectTypesService.GetObjectForUpdareForUser(model.Id, userId);
            return View("Update", model);
        }
    }
}