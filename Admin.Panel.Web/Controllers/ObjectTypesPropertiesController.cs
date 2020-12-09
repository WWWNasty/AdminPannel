using System.Collections.Generic;
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

        public ObjectTypesPropertiesController(
            IQuestionaryObjectTypesRepository questionaryObjectTypesRepository,
            IQuestionaryObjectTypesService questionaryObjectTypesService,
            ILogger<QuestionaryController> logger)
        { 
            _questionaryObjectTypesRepository = questionaryObjectTypesRepository;
            _questionaryObjectTypesService = questionaryObjectTypesService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, TypesObjectRead")]
        public async Task<ActionResult> GetAll()
        {
            List<QuestionaryObjectType> model = await _questionaryObjectTypesRepository.GetAllAsync();
            return View(model);
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
            return View();
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
            //model = await _questionaryObjectTypesService.GetAllProperties();
            return View(model);
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
    }
}