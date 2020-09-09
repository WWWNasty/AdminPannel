using System;
using System.Collections.Generic;
using System.Text;
using Admin.Panel.Core.Interfaces.Repositories.Questionary;
using Admin.Panel.Core.Interfaces.Services;

namespace Admin.Panel.Core.Services
{
    public class QuestionaryObjectService: IQuestionaryObjectService
    {
        private readonly IQuestionaryObjectRepository _questionaryObjectRepository;

        public QuestionaryObjectService(IQuestionaryObjectRepository questionaryObjectRepository)
        {
            _questionaryObjectRepository = questionaryObjectRepository;
        }


    }
}
