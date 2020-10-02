using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.Panel.Core.Entities.Questionary.Questions
{
    public class SelectableAnswersLists: BaseEntity
    {
        [Required(ErrorMessage = "Поле Название - обязательное!")]
        [StringLength(250, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 2)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Список ответов активен")]
        public bool IsUsed { get; set; }
        
        [Required(ErrorMessage = "Не указаны значения!")]
        [MaxLength(30)]
        public List<SelectableAnswers> SelectableAnswers { get; set; }

        [Required(ErrorMessage = "Выбрать допустимые контроллы!")]
        public List<int> InputFieldTypesesId { get; set; }

        [Display(Name = "Список вариантов контроллов для ввода ответа")]
        public List<QuestionaryInputFieldTypes> QuestionaryInputFieldTypeses { get; set; }
        
        public List<QuestionaryInputFieldTypes> SelectedQuestionaryInputFieldTypeses { get; set; }
    }
}