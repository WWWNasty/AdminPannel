using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Panel.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionaryObjectApiController : Controller
    {
        private readonly IQuestionaryObjectRepository _questionaryObjectRepository;

        public QuestionaryObjectApiController(IQuestionaryObjectRepository questionaryObjectRepository)
        {
            _questionaryObjectRepository = questionaryObjectRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<QuestionaryObject> Post([FromBody] QuestionaryObject questionaryObject)
        {
            return await _questionaryObjectRepository.CreateAsync(questionaryObject);
        }
    }
}