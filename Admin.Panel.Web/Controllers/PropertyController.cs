using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.Questionary;
using Admin.Panel.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Panel.Web.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IObjectPropertiesRepository _objectPropertiesRepository;
        private readonly IPropertyService _propertyService;

        public PropertyController(IObjectPropertiesRepository objectPropertiesRepository, IPropertyService propertyService)
        {
            _objectPropertiesRepository = objectPropertiesRepository;
            _propertyService = propertyService;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            return View("Properties");
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ObjectProperty model = new ObjectProperty();
            model.ObjectProperties = await _objectPropertiesRepository.GetAllAsync();
            return View("Properties",model);
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ObjectProperty model)
        {
            if (ModelState.IsValid)
            {
                await _objectPropertiesRepository.CreateAsync(model);
                return RedirectToAction("Create", "Property");
            }
            return View("Properties", model);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _objectPropertiesRepository.GetAsync(id);

            return View("",model);
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(ObjectProperty model)
        {
            if (ModelState.IsValid)
            {
                await _objectPropertiesRepository.UpdateAsync(model);
                return RedirectToAction("Update", "Property");
            }
            return View("Properties",model);
        }
    }
}