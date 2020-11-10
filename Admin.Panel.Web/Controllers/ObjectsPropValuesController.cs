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
    public class ObjectsPropValuesController : Controller
    {
        private readonly IQuestionaryObjectRepository _questionaryObjectRepository;
        private readonly IQuestionaryObjectService _questionaryObjectService;

        public ObjectsPropValuesController(
            IQuestionaryObjectRepository questionaryObjectRepository,
            IQuestionaryObjectService questionaryObjectService)
        {
            _questionaryObjectRepository = questionaryObjectRepository;
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
            QuestionaryObject model = new QuestionaryObject();
            model = await _questionaryObjectService.GetAllForCreate(model);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "ObjectEdit")]
        public async Task<IActionResult> CreateForUser()
        {
            QuestionaryObject model = new QuestionaryObject();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model = await _questionaryObjectService.GetAllForCreateForUser(model, userId);
            return View("Create", model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(QuestionaryObject model)
        {
            if (ModelState.IsValid)
            {
                 var isCodeUnique = await _questionaryObjectRepository.IsCodeUnique(model);
                if (isCodeUnique)
                {
                    await _questionaryObjectRepository.CreateAsync(model);
                    return RedirectToAction("GetAll", "ObjectsPropValues");
                }
                model = await _questionaryObjectService.GetAllForCreate(model);
                model.IsCodeUnique = false;
                return View("Create", model);     
                
            }

            model = await _questionaryObjectService.GetAllForCreate(model);
            return View("Create", model);
        }

        [HttpPost]
        [Authorize(Roles = "ObjectEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateForUser(QuestionaryObject model)
        {
            if (ModelState.IsValid)
            { 
                var isCodeUnique = await _questionaryObjectRepository.IsCodeUnique(model);
                if (isCodeUnique)
                {
                    await _questionaryObjectRepository.CreateAsync(model);
                    return RedirectToAction("GetAllForUser", "ObjectsPropValues");
                }

                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model = await _questionaryObjectService.GetAllForCreateForUser(model, user);
                model.IsCodeUnique = false;
                return View("Create", model);
                
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model = await _questionaryObjectService.GetAllForCreateForUser(model, userId);
            return View("Create", model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<ActionResult> Update(int id)
        {
            var model = await _questionaryObjectRepository.GetAsync(id);
            var isCodeUnique = await _questionaryObjectRepository.IsCodeUnique(model);
            model = await _questionaryObjectService.GetAllForUpdate(model);
            if (isCodeUnique)
            {
                model.IsCodeUnique = true;
                return View(model);
            }

            model.IsCodeUnique = false;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(QuestionaryObject model)
        {
            if (ModelState.IsValid)
            {
                var isCodeUnique = await _questionaryObjectRepository.IsCodeUnique(model);
                if (isCodeUnique)
                {
                    await _questionaryObjectRepository.UpdateAsync(model);
                    return RedirectToAction("GetAll", "ObjectsPropValues"); 
                }

                model = await _questionaryObjectService.GetAllForUpdate(model);
                model.IsCodeUnique = false;
                return View("Update", model);
            }

            model = await _questionaryObjectService.GetAllForUpdate(model);
            return View("Update", model);
        }

        [HttpGet]
        [Authorize(Roles = "ObjectEdit")]
        public async Task<ActionResult> UpdateForUser(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await _questionaryObjectRepository.GetAsync(id);
            model = await _questionaryObjectService.GetAllForUpdateForUser(model, userId);
            var isCodeUnique = await _questionaryObjectRepository.IsCodeUnique(model);
            if (isCodeUnique)
            {
                model.IsCodeUnique = true;
                return View("Update", model);
            }

            model.IsCodeUnique = false;
            return View("Update", model);
        }

        [HttpPost]
        [Authorize(Roles = "ObjectEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateForUser(QuestionaryObject model)
        {
            if (ModelState.IsValid)
            {
                var isCodeUnique = await _questionaryObjectRepository.IsCodeUnique(model);
                if (isCodeUnique)
                {
                    await _questionaryObjectRepository.UpdateAsync(model);
                    return RedirectToAction("GetAllForUser", "ObjectsPropValues");
                }
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model = await _questionaryObjectService.GetAllForUpdateForUser(model, user);
                model.IsCodeUnique = false;
                return View("Update", model);
                
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model = await _questionaryObjectService.GetAllForUpdateForUser(model, userId);
            return View("Update", model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetObjectProperties(int id)
        {
            List<ObjectPropertyValues> prop = await _questionaryObjectRepository.GetPropertiesForUpdate(id);
            QuestionaryObject model = new QuestionaryObject {SelectedObjectPropertyValues = prop};
            return PartialView("_Properties", model);
        }
    }
}