using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Panel.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectTypeApiController : Controller
    {
        private readonly IQuestionaryObjectTypesRepository _objectTypesRepository;

        public ObjectTypeApiController(IQuestionaryObjectTypesRepository objectTypesRepository)
        {
            _objectTypesRepository = objectTypesRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<QuestionaryObjectType> Post([FromBody] QuestionaryObjectType objType)
        {
            return await _objectTypesRepository.CreateAsync(objType);
        }
    }
}