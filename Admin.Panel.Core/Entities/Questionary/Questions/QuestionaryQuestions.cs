using System;
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
        [Range(1, int.MaxValue, ErrorMessage = "Выберите Тип ввода!")]
        public int QuestionaryInputFieldTypeId { get; set; }
        
        [Display(Name = "Вопрос обязательный")]
        public bool CanSkipQuestion { get; set; }
        
        [Required(ErrorMessage = "Тип списка ответов!")]
        [Range(1, 99999, ErrorMessage = "Тип списка ответов!")]
        public int SelectableAnswersListId { get; set; }
        
        public string QuestionaryInputFieldTypeName { get; set; }
  
        public int SequenceOrder { get; set; }
        
        [Display(Name = "Вопрос используется в анкете")]
        public bool IsUsed { get; set; }

        public List<QuestionaryInputFieldTypes> CurrentQuestionaryInputFieldTypes { get; set; }

        public List<SelectableAnswers> CurrentSelectableAnswerses { get; set; }
        
        public List<QuestionaryAnswerOptions> QuestionaryAnswerOptions { get; set; } = new List<QuestionaryAnswerOptions>();
        
        // [Display(Name = "Ответ по умолчанию")]
        public int? DefaultAnswerId { get; set; }

        public string Key => Guid.NewGuid().ToString();
    }
}