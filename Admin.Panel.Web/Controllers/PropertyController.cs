using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.Questionary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Panel.Web.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IObjectPropertiesRepository _objectPropertiesRepository;

        public PropertyController(IObjectPropertiesRepository objectPropertiesRepository)
        {
            _objectPropertiesRepository = objectPropertiesRepository;
        }
        
        [HttpGet]
        [Authorize(Roles = "Админ")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Админ")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ObjectProperty model)
        {
            if (ModelState.IsValid)
            {
                await _objectPropertiesRepository.CreateAsync(model);
                return RedirectToAction("GetAll", "Company");
            }
            return View(model);
        }
        
        [HttpGet]
        [Authorize(Roles = "Админ")]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _objectPropertiesRepository.GetAsync(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Админ")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(ObjectProperty model)
        {
            if (ModelState.IsValid)
            {
                await _objectPropertiesRepository.UpdateAsync(model);
                return RedirectToAction("GetAll", "Company");
            }
            return View(model);
        }
    }
}