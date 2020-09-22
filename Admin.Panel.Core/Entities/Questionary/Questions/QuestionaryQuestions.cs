using System.Collections.Generic;

namespace Admin.Panel.Core.Entities.Questionary.Questions
{
    public class QuestionaryQuestions: BaseEntity
    {
        public int QuestionaryId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionaryInputFieldTypeId { get; set; }
        public bool CanSkipQuestion { get; set; }
        public int SelectableAnswersListId { get; set; }
        
        public string QuestionaryInputFieldTypeName { get; set; }
        public int SequenceOrder { get; set; }
        public int IsUsed { get; set; }
        
        public string SelectableAnswersListName { get; set; }

        public List<SelectableAnswers> SelectableAnswers { get; set; }
        
        
    }
}