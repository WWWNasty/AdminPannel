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
        [Authorize]
        public async Task<ActionResult> Get(int id)
        {
            QuestionaryObject model = await _questionaryObjectRepository.GetAsync(id);
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            List<QuestionaryObject> model = await _questionaryObjectRepository.GetAllAsync();
            return View(model);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            try
            {
                var model =await _questionaryObjectService.GetAllForCreate();
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("", "");
            }
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(QuestionaryObject model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryObjectRepository.CreateAsync(model);
                return RedirectToAction("GetAll", "ObjectsPropValues");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Update(int id)
        {
            var model = await _questionaryObjectService.GetAllForUpdate(id);
            //TODO  доставать валью этого проперти - это должен делать сервис
            
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(QuestionaryObject model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryObjectRepository.UpdateAsync(model);
                return RedirectToAction("GetAll", "ObjectsPropValues");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetObjectProperties(int id)
        {
            //выбрать проперти по id типа объекта
          List<ObjectPropertyValues> prop =  await _questionaryObjectRepository.GetPropertiesForUpdate(id);
          QuestionaryObject model = new QuestionaryObject();
          model.SelectedObjectPropertyValues = prop;
          return PartialView("_Properties",model);
        }

    }
}
