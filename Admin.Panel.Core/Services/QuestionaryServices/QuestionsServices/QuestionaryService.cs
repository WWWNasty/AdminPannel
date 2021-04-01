using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Entities.Questionary.Questions;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces.QuestionsRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces.QuestionsServiceInterfaces;

namespace Admin.Panel.Core.Services.QuestionaryServices.QuestionsServices
{
    public class QuestionaryService : IQuestionaryService
    {
        private readonly IQuestionaryObjectTypesRepository _questionaryObjectTypesRepository;
        private readonly ISelectableAnswersListRepository _selectableAnswersListRepository;
        private readonly IQuestionaryInputFieldTypesRepository _questionaryInputFieldTypesRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IQuestionaryRepository _questionaryRepository;
        private readonly IQuestionaryInputFieldTypesRepository _fieldTypesRepository;
        private readonly IQuestionaryObjectRepository _questionaryObjectRepository;
        private readonly IObjectPropertiesRepository _objectPropertiesRepository;

        public QuestionaryService(
            IQuestionaryObjectTypesRepository questionaryObjectTypesRepository,
            ISelectableAnswersListRepository selectableAnswersListRepository,
            IQuestionaryInputFieldTypesRepository questionaryInputFieldTypesRepository,
            ICompanyRepository companyRepository,
            IQuestionaryRepository questionaryRepository,
            IQuestionaryInputFieldTypesRepository fieldTypesRepository,
            IQuestionaryObjectRepository questionaryObjectRepository,
            IObjectPropertiesRepository objectPropertiesRepository)
        {
            _questionaryObjectTypesRepository = questionaryObjectTypesRepository;
            _selectableAnswersListRepository = selectableAnswersListRepository;
            _questionaryInputFieldTypesRepository = questionaryInputFieldTypesRepository;
            _companyRepository = companyRepository;
            _questionaryRepository = questionaryRepository;
            _fieldTypesRepository = fieldTypesRepository;
            _questionaryObjectRepository = questionaryObjectRepository;
            _objectPropertiesRepository = objectPropertiesRepository;
        }

        private async Task<QuestionaryDto> GetAllForQuestionary(int objectTypeId)
        {
            QuestionaryDto obj = new QuestionaryDto();
            obj.ApplicationCompanies = await _companyRepository.GetAllActiveAsync();
            obj.QuestionaryObjectTypes =
                await _questionaryObjectTypesRepository.GetAllActiveWithoutQuestionaryAsync(objectTypeId);
            obj.SelectableAnswersLists = await _selectableAnswersListRepository.GetAllActiveAsync();
            obj.QuestionaryObjects = await _questionaryObjectRepository.GetAllAsync();
            obj.ObjectProperties = await _objectPropertiesRepository.GetAllActiveAsync();
            obj.SelectableAnswers = await _selectableAnswersListRepository.GetAllAnswers();
            obj.QuestionaryInputFieldTypes = await _questionaryInputFieldTypesRepository.GetAllWithListId();
            return obj;
        }

        private async Task<QuestionaryDto> GetAllForQuestionaryUser(string idUser, int objectTypeId)
        {
            QuestionaryDto obj = new QuestionaryDto();
            obj.ApplicationCompanies = await _companyRepository.GetAllActiveForUserAsync(idUser);
            obj.QuestionaryObjectTypes =
                await _questionaryObjectTypesRepository.GetAllActiveWithoutQuestionaryForUserAsync(
                    Convert.ToInt32(idUser), objectTypeId);
            obj.SelectableAnswersLists = await _selectableAnswersListRepository.GetAllActiveAsync();
            obj.QuestionaryObjects =
                await _questionaryObjectRepository.GetAllForUserAsync(Convert.ToInt32(idUser));
            obj.ObjectProperties = await _objectPropertiesRepository.GetAllActiveAsync();
            obj.SelectableAnswers = await _selectableAnswersListRepository.GetAllAnswers();
            obj.QuestionaryInputFieldTypes = await _questionaryInputFieldTypesRepository.GetAllWithListId();
            return obj;
        }

        public async Task<QuestionaryDto> GetAllForQuestionaryUpdate(QuestionaryDto model)
        {
            var obj = await GetAllForQuestionary(model.ObjectTypeId);
            model.ApplicationCompanies = obj.ApplicationCompanies;
            model.QuestionaryObjectTypes = obj.QuestionaryObjectTypes;
            //получить все объекты и пропсы для типов объектов
            foreach (var objectType in model.QuestionaryObjectTypes)
            {
                objectType.ObjectProperties =
                    obj.ObjectProperties.Where(p => p.QuestionaryObjectTypeId == objectType.Id).ToList();
                objectType.QuestionaryObjects =
                    obj.QuestionaryObjects.Where(o => o.ObjectTypeId == objectType.Id).ToList();
            }

            model.ObjectsIdToChangeType = model.QuestionaryObjectTypes.FirstOrDefault(t => t.Id == model.ObjectTypeId)
                ?.QuestionaryObjects?.Where(o => o.IsUsed).Select(o => o.Id).ToArray();
            model.SelectableAnswersLists = obj.SelectableAnswersLists;
            model.SelectableAnswers = obj.SelectableAnswers;
            model.QuestionaryInputFieldTypes = obj.QuestionaryInputFieldTypes;
            if (model.QuestionaryQuestions != null)
            {
                foreach (QuestionaryQuestions question in model.QuestionaryQuestions)
                {
                    //question.CanSkipQuestion = !question.CanSkipQuestion;
                    if (question.SelectableAnswersListId != 0)
                    {
                        List<QuestionaryInputFieldTypes> currentInputFields =
                            await _questionaryInputFieldTypesRepository.GetAllCurrent(question.SelectableAnswersListId);
                        question.CurrentQuestionaryInputFieldTypes = currentInputFields;

                        List<SelectableAnswers> current =
                            await _selectableAnswersListRepository.GetSelectableAnswersAsync(question
                                .SelectableAnswersListId);
                        question.CurrentSelectableAnswerses = current;
                    }
                }
            }

            return model;
        }

        public async Task<QuestionaryDto> GetAllForQuestionaryCreate(QuestionaryDto model)
        {
            var allForQuestionary = await GetAllForQuestionary(model.ObjectTypeId);
            model.ApplicationCompanies = allForQuestionary.ApplicationCompanies;
            model.QuestionaryObjectTypes = allForQuestionary.QuestionaryObjectTypes;
            foreach (var objectType in model.QuestionaryObjectTypes)
            {
                objectType.ObjectProperties = allForQuestionary.ObjectProperties
                    .Where(p => p.QuestionaryObjectTypeId == objectType.Id).ToList();
                objectType.QuestionaryObjects = allForQuestionary.QuestionaryObjects
                    .Where(o => o.ObjectTypeId == objectType.Id).ToList();
            }

            model.SelectableAnswersLists = allForQuestionary.SelectableAnswersLists;
            model.SelectableAnswers = allForQuestionary.SelectableAnswers;
            model.QuestionaryInputFieldTypes = allForQuestionary.QuestionaryInputFieldTypes;
            return model;
        }

        public async Task<QuestionaryDto> GetAllForQuestionaryForUserUpdate(QuestionaryDto model, string idUser)
        {
            var allForObj = await GetAllForQuestionaryUser(idUser, model.ObjectTypeId);
            model.ApplicationCompanies = allForObj.ApplicationCompanies;
            model.QuestionaryObjectTypes = allForObj.QuestionaryObjectTypes;
            //получить все объекты и пропсы для типов объектов в репозитории
            foreach (var objectType in model.QuestionaryObjectTypes)
            {
                objectType.ObjectProperties = allForObj.ObjectProperties
                    .Where(p => p.QuestionaryObjectTypeId == objectType.Id).ToList();
                objectType.QuestionaryObjects =
                    allForObj.QuestionaryObjects.Where(o => o.ObjectTypeId == objectType.Id).ToList();
            }

            model.ObjectsIdToChangeType = model.QuestionaryObjectTypes.FirstOrDefault(t => t.Id == model.ObjectTypeId)
                ?.QuestionaryObjects.Where(o => o.IsUsed).Select(o => o.Id).ToArray();
            model.SelectableAnswers = allForObj.SelectableAnswers;
            model.QuestionaryInputFieldTypes = allForObj.QuestionaryInputFieldTypes;
            model.SelectableAnswersLists = allForObj.SelectableAnswersLists;

            if (model.QuestionaryQuestions != null)
            {
                foreach (QuestionaryQuestions question in model.QuestionaryQuestions)
                {
                    if (question.SelectableAnswersListId != 0)
                    {
                        List<QuestionaryInputFieldTypes> currentInputFields =
                            await _questionaryInputFieldTypesRepository.GetAllCurrent(question.SelectableAnswersListId);
                        question.CurrentQuestionaryInputFieldTypes = currentInputFields;

                        List<SelectableAnswers> current =
                            await _selectableAnswersListRepository.GetSelectableAnswersAsync(question
                                .SelectableAnswersListId);
                        question.CurrentSelectableAnswerses = current;
                    }
                }
            }

            return model;
        }

        public async Task<QuestionaryDto> GetAllForQuestionaryForUserCreate(QuestionaryDto model, string idUser)
        {
            var obj = await GetAllForQuestionaryUser(idUser, model.ObjectTypeId);
            model.ApplicationCompanies = obj.ApplicationCompanies;
            model.QuestionaryObjectTypes = obj.QuestionaryObjectTypes;
            foreach (var objectType in model.QuestionaryObjectTypes)
            {
                objectType.ObjectProperties =
                    obj.ObjectProperties.Where(p => p.QuestionaryObjectTypeId == objectType.Id).ToList();
                objectType.QuestionaryObjects =
                    obj.QuestionaryObjects.Where(o => o.ObjectTypeId == objectType.Id).ToList();
            }

            model.SelectableAnswersLists = obj.SelectableAnswersLists;
            model.SelectableAnswers = obj.SelectableAnswers;
            model.QuestionaryInputFieldTypes = obj.QuestionaryInputFieldTypes;

            return model;
        }

        public Task<bool> IfQuestionaryCurrentInCompany(int idCompany, int idObjType, int idQuestionary)
        {
            var current =
                _questionaryRepository.IfQuestionaryCurrentInCompanyAsync(idCompany, idObjType, idQuestionary);
            return current;
        }

        public async Task<QuestionaryDto> AnswersGetAll(int id, int index, int qqId)
        {
            QuestionaryDto model = new QuestionaryDto();
            model.QuestionaryInputFieldTypes = await _fieldTypesRepository.GetAllCurrent(id);
            model.IndexCurrentQuestion = index;
            foreach (var _ in Enumerable.Range(0, index + 1))
            {
                model.QuestionaryQuestions.Add(new QuestionaryQuestions());
            }

            model.SelectableAnswers = await _selectableAnswersListRepository.GetSelectableAnswersAsync(id);
            return model;
        }

        public async Task<QuestionaryDto> ObjectTypesGetAll(int id)
        {
            QuestionaryDto model = new QuestionaryDto();
            model.QuestionaryObjectTypes = await _questionaryObjectTypesRepository.GetAllCurrent(id);
            return model;
        }
    }
}