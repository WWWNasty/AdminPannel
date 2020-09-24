using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.Panel.Core.Entities.Questionary.Questions
{
    public class QuestionaryQuestions: BaseEntity
    {
        public int QuestionaryId { get; set; }
        
        [Required(ErrorMessage = "Поле Текст вопроса - обязательно!")]
        [StringLength(500, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 2)]
        [Display(Name = "Текст вопроса")]
        public string QuestionText { get; set; }
        
        [Required(ErrorMessage = "Выберите Тип ввода!")]
        public int QuestionaryInputFieldTypeId { get; set; }
        
        [Display(Name = "Вопрос обязательный")]
        public bool CanSkipQuestion { get; set; }
        
        [Required(ErrorMessage = "Тип списка ответов!")]
        public int SelectableAnswersListId { get; set; }
        
        public string QuestionaryInputFieldTypeName { get; set; }
        
        [Required(ErrorMessage = "Укажите порядок следования!")]
        [Range(0, 29, ErrorMessage = "Недопустимый индекс")]
        [Display(Name = "Сортировка")]
        public int SequenceOrder { get; set; }
        
        [Display(Name = "Вопрос используется в анкете")]
        public bool IsUsed { get; set; }
        
        public string SelectableAnswersListName { get; set; }
        
        public List<SelectableAnswers> SelectableAnswers { get; set; }
        
        
    }
}