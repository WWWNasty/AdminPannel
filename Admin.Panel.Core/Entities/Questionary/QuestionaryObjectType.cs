using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    public class QuestionaryObjectType: BaseEntity
    {
        [Required(ErrorMessage = "Поле Название - обязательно!")]
        [StringLength(250, ErrorMessage = "Длина должна быть не менее {0} символов.", MinimumLength = 1)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Display(Name = "Свойства")]
        public List<ObjectProperty> SelectedObjectProperties { get; set; }
        
        [Display(Name = "Создать новое свойство")]
        public List<ObjectProperty> ObjectProperties { get; set; }
        
        [Required(ErrorMessage = "Поле обязательно!")]
        public int CompanyId { get; set; }
        
        public string CompanyName { get; set; }
        
        [Display(Name = "Компания")]
        public List<ApplicationCompany> Companies { get; set; }

        public List<QuestionaryObject> QuestionaryObjects { get; set; }

        [Display(Name = "Активный")]
        public bool IsUsed { get; set; }
    }
}
