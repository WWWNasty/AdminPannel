using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Panel.Web.Controllers
{
    public class ObjectsPropValuesController : Controller
    {
        private readonly IQuestionaryObjectRepository _questionaryObjectRepository;
        private readonly IQuestionaryObjectService _questionaryObjectService;
        private readonly ICompanyRepository _companyRepository;
        private readonly IObjectPropertiesRepository _objectPropertiesRepository;

        public ObjectsPropValuesController(
            IQuestionaryObjectRepository questionaryObjectRepository,
            ICompanyRepository companyRepository,
            IObjectPropertiesRepository objectPropertiesRepository,
            IQuestionaryObjectService questionaryObjectService)
        {
            _questionaryObjectRepository = questionaryObjectRepository;
            _companyRepository = companyRepository;
            _objectPropertiesRepository = objectPropertiesRepository;
            _questionaryObjectService = questionaryObjectService;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, ObjectRead")]
        public async Task<ActionResult> Get(int id)
        {
            QuestionaryObject model = await _questionaryObjectRepository.GetAsync(id);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<ActionResult> GetAll()
        {
            List<QuestionaryObject> model = await _questionaryObjectRepository.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "ObjectRead")]
        public async Task<ActionResult> GetAllForUser()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<QuestionaryObject> model = await _questionaryObjectRepository.GetAllForUserAsync(userId);
            return View("GetAll", model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<IActionResult> Create()
        {
            try
            {
                var model = await _questionaryObjectService.GetAllForCreate();
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("", "");
            }
        }

        [HttpGet]
        [Authorize(Roles = "ObjectEdit")]
        public async Task<IActionResult> CreateForUser()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var model = await _questionaryObjectService.GetAllForCreateForUser(userId);
                return View("Create", model);
            }
            catch (Exception)
            {
                return RedirectToAction("", "");
            }
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(QuestionaryObject model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryObjectRepository.CreateAsync(model);
                return RedirectToAction("GetAll", "ObjectsPropValues");
            }
            model = await _questionaryObjectService.GetAllForCreate();
            return View("Create", model);
        }

        [HttpPost]
        [Authorize(Roles = "ObjectEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateForUser(QuestionaryObject model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryObjectRepository.CreateAsync(model);
                return RedirectToAction("GetAllForUser", "ObjectsPropValues");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model = await _questionaryObjectService.GetAllForCreateForUser(userId);
            return View("Create", model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<ActionResult> Update(int id)
        {
            var model = await _questionaryObjectService.GetAllForUpdate(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(QuestionaryObject model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryObjectRepository.UpdateAsync(model);
                return RedirectToAction("GetAll", "ObjectsPropValues");
            }
            var mod = await _questionaryObjectService.GetAllForUpdate(model.Id);
            
            model.Companies = mod.Companies;
            model.QuestionaryObjectTypes = mod.QuestionaryObjectTypes;
            return View("Update", model);
        }

        [HttpPost]
        [Authorize(Roles = "ObjectEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateForUser(QuestionaryObject model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryObjectRepository.UpdateAsync(model);
                return RedirectToAction("GetAllForUser", "ObjectsPropValues");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var mod = await _questionaryObjectService.GetAllForUpdateForUser(model.Id ,userId);
            
            model.Companies = mod.Companies;
            model.QuestionaryObjectTypes = mod.QuestionaryObjectTypes;
            return View("Update", model);
        }

        [HttpGet]
        [Authorize(Roles = "ObjectEdit")]
        public async Task<ActionResult> UpdateForUser(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await _questionaryObjectService.GetAllForUpdateForUser(id, userId);
            return View("Update", model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetObjectProperties(int id)
        {
            List<ObjectPropertyValues> prop = await _questionaryObjectRepository.GetPropertiesForUpdate(id);
            QuestionaryObject model = new QuestionaryObject();
            model.SelectedObjectPropertyValues = prop;
            return PartialView("_Properties", model);
        }
    }
}