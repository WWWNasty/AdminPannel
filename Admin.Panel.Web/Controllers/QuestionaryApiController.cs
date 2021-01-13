using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Entities.Questionary.Questions;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Admin.Panel.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionaryApiController : Controller
    {
        private readonly IQuestionaryService _questionaryService;
        private readonly IQuestionaryObjectTypesRepository _objectTypesRepository;
        private readonly ILogger<QuestionaryApiController> _logger;

        public QuestionaryApiController(
            IQuestionaryObjectTypesRepository objectTypesRepository, 
            ILogger<QuestionaryApiController> logger, 
            IQuestionaryService questionaryService)
        {
            _objectTypesRepository = objectTypesRepository;
            _logger = logger;
            _questionaryService = questionaryService;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator")]
        public async Task<ActionResult<QuestionaryDto>> Get()
        {
            QuestionaryDto model = new QuestionaryDto();
            model = await _questionaryService.GetAllForQuestionaryCreate(model);
            return Ok(model);
        }
    }
}