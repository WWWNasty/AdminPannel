using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;

namespace Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces
{
    public interface IQuestionaryObjectTypesService
    {
        //Task<QuestionaryObjectType> GetAllProperties();
        Task<QuestionaryObjectType> GetObjectForUpdare(int id);
        Task<QuestionaryObjectType> GetObjectForUpdareForUser(int id, string userId);
    }
}
