using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.Questionary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Panel.Web.Controllers
{
    public class ObjectTypesPropertiesController : Controller
    {
        //private readonly IObjectPropertiesRepository _objectPropertiesRepository;
        private readonly IQuestionaryObjectTypesRepository _questionaryObjectTypesRepository;

        public ObjectTypesPropertiesController(IObjectPropertiesRepository objectPropertiesRepository, IQuestionaryObjectTypesRepository questionaryObjectTypesRepository)
        {
            //_objectPropertiesRepository = objectPropertiesRepository;
            _questionaryObjectTypesRepository = questionaryObjectTypesRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Админ")]
        public async Task<ActionResult> GetAll()
        {
            List<QuestionaryObjectType> model = await _questionaryObjectTypesRepository.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Админ")]
        public async Task<ActionResult> Get(int id)
        {
            QuestionaryObjectType model = await _questionaryObjectTypesRepository.GetAsync(id);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Админ")]
        public async Task<IActionResult> Create()
        {
            //TODO получить все проперти из  проперти репозит
            //var model = await _questionaryObjectTypesRepository.GetAsync(id);

            //var model = new UpdateUserViewModel()
            //{
            //    Id = user.Id,
            //    IsUsed = user.IsUsed,
            //    UserName = user.UserName,
            //    Nickname = user.Nickname,
            //    Email = user.Email,
            //    //CreatedDate = user.CreatedDate,
            //    //ApplicationCompanyId = user.ApplicationCompanyId

            //};
            //return View(_mapper.Map<UpdateUserViewModel>(user));
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Админ")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(QuestionaryObjectType model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryObjectTypesRepository.CreateAsync(model);
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Админ")]
        public async Task<IActionResult> Update(int id)
        {
            //TODO получить все проперти из  проперти репозит
            var model = await _questionaryObjectTypesRepository.GetAsync(id);

            //var model = new UpdateUserViewModel()
            //{
            //    Id = user.Id,
            //    IsUsed = user.IsUsed,
            //    UserName = user.UserName,
            //    Nickname = user.Nickname,
            //    Email = user.Email,
            //    //CreatedDate = user.CreatedDate,
            //    //ApplicationCompanyId = user.ApplicationCompanyId

            //};
            //return View(_mapper.Map<UpdateUserViewModel>(user));
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Админ")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(QuestionaryObjectType model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryObjectTypesRepository.UpdateAsync(model);
                return View(model);
            }
            return View(model);
        }
    }
}
