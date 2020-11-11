using System.Security.Claims;
using System.Threading.Tasks;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.Completed;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Panel.Web.Controllers
{
    public class CompletedQuestionaryController : Controller
    {
        private readonly ICompletedQuestionaryRepository _completedQuestionaryRepository;

        public CompletedQuestionaryController(ICompletedQuestionaryRepository completedQuestionaryRepository)
        {
            _completedQuestionaryRepository = completedQuestionaryRepository;
        }
        
        [HttpGet]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<ActionResult> GetAll()
        {
            var model = await _completedQuestionaryRepository.GetAllAsync();
            return View(model);
        }

        // [HttpGet]
        // [Authorize(Roles = "SuperAdministrator, QuestionaryRead")]
        // public async Task<ActionResult> GetAllForUser()
        // {
        //     var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //     var model = await _completedQuestionaryRepository.GetAllForUserAsync(userId);
        //     return View("GetAll", model);
        // }
        
    }
}