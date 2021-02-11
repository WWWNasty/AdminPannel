namespace Admin.Panel.Core.Entities.Questionary.Questions
{
    public class QuestionaryInputFieldTypes: BaseEntity
    {
        public string Name { get; set; }
        
        //
        public int SelectableAnswersListId { get; set; }
    }
}