namespace Admin.Panel.Core.Entities.Questionary.Questions
{
    public class QuestionaryAnswerOptions: BaseEntity
    {
        public int QuestionaryId { get; set; }
        public int SelectableAnswerId { get; set; }
        public bool IsInvolvesComment { get; set; }
    }
}