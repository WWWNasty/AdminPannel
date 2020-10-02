using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces;
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
        [Authorize(Roles = "SuperAdministrator, PropertiesObjectRead")]
        public async Task<ActionResult> GetAll()
        {
            List<ObjectProperty> prop = await _objectPropertiesRepository.GetAllAsync();
            ObjectProperty model = new ObjectProperty();
            model.ObjectProperties = prop;
            return View("Properties", model);
        }
        
        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, PropertiesObjectEdit, PropertiesObjectRead")]
        public async Task<IActionResult> Create()
        {
            ObjectProperty model = new ObjectProperty();
            model.ObjectProperties = await _objectPropertiesRepository.GetAllAsync();
            return View("Properties",model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator, PropertiesObjectEdit, PropertiesObjectRead")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ObjectProperty model)
        {
            if (ModelState.IsValid)
            {
                await _objectPropertiesRepository.CreateAsync(model);
                return RedirectToAction("Create", "Property");
            }
            model.ObjectProperties = await _objectPropertiesRepository.GetAllAsync();
            return View("Properties", model);
        }
        
        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, PropertiesObjectEdit")]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _objectPropertiesRepository.GetAsync(id);

            return View("Update",model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator, PropertiesObjectEdit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(ObjectProperty model)
        {
            if (ModelState.IsValid)
            {
                await _objectPropertiesRepository.UpdateAsync(model);
                return RedirectToAction("GetAll", "Property");
            }
            return RedirectToAction("GetAll", "Property");
        }
    }
}