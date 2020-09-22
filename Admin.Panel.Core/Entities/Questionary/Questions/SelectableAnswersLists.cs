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
        
        [Required(ErrorMessage = "Не указаны значения!")]
        [MaxLength(30)]
        public SelectableAnswers[] SelectableAnswers { get; set; } = new SelectableAnswers[1];
    }
}