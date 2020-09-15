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
        [Authorize(Roles = "SuperAdministrator")]
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
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator")]
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
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _companyRepository.GetAsync(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator")]
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
        
    }
}
