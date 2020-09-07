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
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Админ")]
        public async Task<ActionResult> GetAll()
        {
            List<ApplicationCompany> model = await _companyRepository.GetAllAsync();
            return View(model);
        }

        //[HttpGet]
        //[Authorize(Roles = "Админ")]
        //public async Task<ActionResult> Get(int id)
        //{
        //    ApplicationCompany model = await _companyRepository.GetAsync(id);
        //    return View(model);
        //}

        [HttpGet]
        [Authorize(Roles = "Админ")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Админ")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ApplicationCompany model)
        {
            if (ModelState.IsValid)
            {
                await _companyRepository.CreateAsync(model);
                return RedirectToAction("GetAll", "Company");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Админ")]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _companyRepository.GetAsync(id);

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
        public async Task<IActionResult> Update(ApplicationCompany model)
        {
            if (ModelState.IsValid)
            {
                await _companyRepository.UpdateAsync(model);
                return RedirectToAction("GetAll", "Company");
            }
            return View(model);
        }

        
        [Authorize]
        public async Task<IActionResult> Delete(ApplicationCompany company)
        {
            await _companyRepository.DeleteAsync(company);

                //string redirect = Request.Headers["Referer"].ToString();
            return RedirectToAction("GetAll", "Company");
        }
    }
}
