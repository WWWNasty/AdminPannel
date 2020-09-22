using System.Collections.Generic;

namespace Admin.Panel.Core.Entities.Questionary.Questions
{
    public class QuestionaryDto: BaseEntity
    {
        public string Name { get; set; }
        public int ObjectTypeId { get; set; }
        public string ObjectTypeName { get; set; }
        public int CompanyId { get; set; }
        public List<QuestionaryQuestions> QuestionaryQuestions { get; set; }

        public List<ApplicationCompany> ApplicationCompanies { get; set; }
        public List<QuestionaryInputFieldTypes> QuestionaryInputFieldTypes { get; set; }
        public List<SelectableAnswersLists> SelectableAnswersLists { get; set; }
        public List<QuestionaryObjectType> QuestionaryObjectTypes { get; set; }
    }
}