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

        public async Task<QuestionaryObject> GetAllForCreate()
        {
            //TODO компании нужно брать доступные авторизованным пользователем
            List<ApplicationCompany> companies = await _companyRepository.GetAllActiveAsync();
            List<QuestionaryObjectType> objTypes = await _questionaryObjectTypesRepository.GetAllAsync();
            
            QuestionaryObject createObj = new QuestionaryObject
            {
                Companies = companies,
                QuestionaryObjectTypes = objTypes
            };

            return createObj;
        }
        
        public async Task<QuestionaryObject> GetAllForUpdate(int id)
        {
            //TODO компании нужно брать доступные авторизованным пользователем
            var model = await _questionaryObjectRepository.GetAsync(id);

            List<ApplicationCompany> companies = await _companyRepository.GetAllAsync();
            List<QuestionaryObjectType> objTypes = await _questionaryObjectTypesRepository.GetAllAsync();

            model.Companies = companies;
            model.QuestionaryObjectTypes = objTypes;

            return model;
        }

        public Task<List<ObjectPropertyValues>> GetPropertiesForUpdate(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ObjectPropertyValues>> GetPropertiesForCreate()
        {
            throw new NotImplementedException();
        }
    }
}
