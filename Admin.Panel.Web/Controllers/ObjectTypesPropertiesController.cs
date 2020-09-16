using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.Questionary;
using Admin.Panel.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Panel.Web.Controllers
{
    public class ObjectTypesPropertiesController : Controller
    {
        private readonly IQuestionaryObjectTypesRepository _questionaryObjectTypesRepository;
        private readonly IQuestionaryObjectTypesService _questionaryObjectTypesService;

        public ObjectTypesPropertiesController(IQuestionaryObjectTypesRepository questionaryObjectTypesRepository, IQuestionaryObjectTypesService questionaryObjectTypesService)
        {
            _questionaryObjectTypesRepository = questionaryObjectTypesRepository;
            _questionaryObjectTypesService = questionaryObjectTypesService;
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
            QuestionaryObjectType model = await _questionaryObjectTypesService.GetAllProperties();

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
                return RedirectToAction("GetAll", "ObjectTypesProperties");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, TypesObjectEdit")]
        public async Task<IActionResult> Update(int id)
        {
            QuestionaryObjectType model = await _questionaryObjectTypesService.GetObjectForUpdare(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator, TypesObjectEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(QuestionaryObjectType model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryObjectTypesRepository.UpdateAsync(model);
                return RedirectToAction("GetAll", "ObjectTypesProperties");            
            }
            return View(model);
        }
    }
}
