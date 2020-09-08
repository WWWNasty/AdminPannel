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
    public class ObjectsPropValuesController : Controller
    {
        private readonly IQuestionaryObjectRepository _questionaryObjectRepository;
        private readonly ICompanyRepository _companyRepository;

        public ObjectsPropValuesController(IQuestionaryObjectRepository questionaryObjectRepository, ICompanyRepository companyRepository)
        {
            _questionaryObjectRepository = questionaryObjectRepository;
            _companyRepository = companyRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Админ")]
        public async Task<ActionResult> Get(int id)
        {
            QuestionaryObject model = await _questionaryObjectRepository.GetAsync(id);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Админ")]
        public async Task<ActionResult> GetAll()
        {
            List<QuestionaryObject> model = await _questionaryObjectRepository.GetAllAsync();
            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "Админ")]
        public async Task<IActionResult> Create()
        {
            //TODO получить все company , questionaryObjectTypes , objectProperties и доставать валью этого проперти
            try
            {
                List<ApplicationCompany> model = await _companyRepository.GetAllAsync();

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("", "");
            }
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
        public async Task<IActionResult> Create(QuestionaryObject model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryObjectRepository.CreateAsync(model);
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Админ")]
        public async Task<ActionResult> Update(int id)
        {
            var model = await _questionaryObjectRepository.GetAsync(id);
            //TODO получить все cjmpany , questionaryObjectTypes , objectProperties и доставать валью этого проперти
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
        public async Task<IActionResult> Update(QuestionaryObject model)
        {
            if (ModelState.IsValid)
            {
                await _questionaryObjectRepository.UpdateAsync(model);
                return View(model);
            }
            return View(model);
        }

    }
}
