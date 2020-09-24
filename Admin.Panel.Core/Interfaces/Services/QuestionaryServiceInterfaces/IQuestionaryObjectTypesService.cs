﻿using System.Threading.Tasks;
using Admin.Panel.Core.Entities.Questionary;

namespace Admin.Panel.Core.Interfaces.Services.QuestionaryServiceInterfaces
{
    public interface IQuestionaryObjectTypesService
    {
        public Task<QuestionaryObjectType> GetAllProperties();
        public Task<QuestionaryObjectType> GetObjectForUpdare(int id);
    }
}