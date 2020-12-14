using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces;

namespace Admin.Panel.Core.Services.QuestionaryServices
{
    public class QuestionaryObjectService : IQuestionaryObjectService
    {
        private readonly IQuestionaryObjectTypesRepository _questionaryObjectTypesRepository;
        private readonly ICompanyRepository _companyRepository;

        public QuestionaryObjectService(
            ICompanyRepository companyRepository,
            IQuestionaryObjectTypesRepository questionaryObjectTypesRepository)
        {
            _companyRepository = companyRepository;
            _questionaryObjectTypesRepository = questionaryObjectTypesRepository;
        }

        public async Task<QuestionaryObject> GetAllForCreate(QuestionaryObject model)
        {
            List<ApplicationCompany> companies = await _companyRepository.GetAllActiveAsync();
            List<QuestionaryObjectType> objTypes = await _questionaryObjectTypesRepository.GetAllActiveAsync();

            model.QuestionaryObjectTypes = objTypes;
            return model;
        }

        public async Task<QuestionaryObject> GetAllForCreateForUser(QuestionaryObject model, string userId)
        {
            List<QuestionaryObjectType> objTypes = await _questionaryObjectTypesRepository.GetAllActiveForUserAsync(Convert.ToInt32(userId)); 

            model.QuestionaryObjectTypes = objTypes;
                    
            return model;
        }

        public async Task<QuestionaryObject> GetAllForUpdate(QuestionaryObject model)
        {
            List<QuestionaryObjectType> objTypes = await _questionaryObjectTypesRepository.GetAllActiveAsync();

            model.QuestionaryObjectTypes = objTypes;

            return model;
        }

        public async Task<QuestionaryObject> GetAllForUpdateForUser(QuestionaryObject model, string userId)
        {
            List<QuestionaryObjectType> objTypes = await _questionaryObjectTypesRepository.GetAllActiveForUserAsync(Convert.ToInt32(userId));

            model.QuestionaryObjectTypes = objTypes;

            return model;
        }
    }
}