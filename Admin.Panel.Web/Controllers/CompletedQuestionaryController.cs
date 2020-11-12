using System.Security.Claims;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary.Completed;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.Completed;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.Completed;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Panel.Web.Controllers
{
    public class CompletedQuestionaryController : Controller
    {
        private readonly ICompletedQuestionaryRepository _completedQuestionaryRepository;
        private readonly ICompletedQuestionaryService _completedQuestionaryService;

        public CompletedQuestionaryController(
            ICompletedQuestionaryRepository completedQuestionaryRepository, 
            ICompletedQuestionaryService completedQuestionaryService)
        {
            _completedQuestionaryRepository = completedQuestionaryRepository;
            _completedQuestionaryService = completedQuestionaryService;
        }
        
        [HttpGet]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<ActionResult> GetAll()
        {
            QueryParameters model = new QueryParameters();
            model = await _completedQuestionaryService.GetAll(model);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdministrator")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> GetAll(QueryParameters model)
        {
            if (ModelState.IsValid)
            {
                model = await _completedQuestionaryService.GetAll(model);
                return View(model);
            }
            model = await _completedQuestionaryService.GetAll(model);
            return View(model);
        }
        
        // [HttpGet]
        // [Authorize(Roles = "SuperAdministrator, ")]
        // public async Task<ActionResult> GetAllForUser()
        // {
        //     var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //     var model = await _completedQuestionaryRepository.GetAllForUserAsync(userId);
        //     return View("GetAll", model);
        // }
        
    }
}