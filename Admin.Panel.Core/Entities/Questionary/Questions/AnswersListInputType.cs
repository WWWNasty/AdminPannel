namespace Admin.Panel.Core.Entities.Questionary.Questions
{
    public class AnswersListInputType: BaseEntity
    {
        public int SelectableAnswersListId { get; set; }
        public int QuestionaryInputFieldTypeId { get; set; }
    }
}