using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.Questionary;
using Admin.Panel.Core.Interfaces.Services;

namespace Admin.Panel.Core.Services
{
    public class QuestionaryObjectService: IQuestionaryObjectService
    {
        private readonly IQuestionaryObjectTypesRepository _questionaryObjectTypesRepository;
        private readonly IQuestionaryObjectRepository _questionaryObjectRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IObjectPropertiesRepository _objectPropertiesRepository;

        public QuestionaryObjectService(
            IQuestionaryObjectRepository questionaryObjectRepository, 
            ICompanyRepository companyRepository, 
            IObjectPropertiesRepository objectPropertiesRepository, 
            IQuestionaryObjectTypesRepository questionaryObjectTypesRepository)
        {
            _questionaryObjectRepository = questionaryObjectRepository;
            _companyRepository = companyRepository;
            _objectPropertiesRepository = objectPropertiesRepository;
            _questionaryObjectTypesRepository = questionaryObjectTypesRepository;
        }

        public async Task<QuestionaryObject> GetAllForcreate()
        {
            //TODO компании нужно брать доступные авторизованным пользователем
            List<ApplicationCompany> companies = await _companyRepository.GetAllAsync();
            List<QuestionaryObjectType> objTypes = await _questionaryObjectTypesRepository.GetAllAsync();
            
            QuestionaryObject createObj = new QuestionaryObject
            {
                Companies = companies,
                QuestionaryObjectTypes = objTypes
            };

            return createObj;
        }

    }
}
