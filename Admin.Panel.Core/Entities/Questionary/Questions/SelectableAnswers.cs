using System.ComponentModel.DataAnnotations;

namespace Admin.Panel.Core.Entities.Questionary.Questions
{
    public class SelectableAnswers: BaseEntity
    {
        public int SelectableAnswersListId { get; set; }
        
        [Required(ErrorMessage = "Поле Текст вопроса - обязательно!")]
        [StringLength(250, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 4)]
        [Display(Name = "Текст вопроса")]
        public string AnswerText { get; set; }
        
        [Display(Name = "Ответ по умолчанию")]
        public bool IsDefaultAnswer { get; set; }
        
        [Display(Name = "Комментарий к вопросу")]
        public bool IsInvolvesComment { get; set; }
        
        [Required(ErrorMessage = "Укажите порядок следования!")]
        [StringLength(2, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 1)]
        [Display(Name = "Сортировка")]
        public int SequenceOrder { get; set; }
    }
}