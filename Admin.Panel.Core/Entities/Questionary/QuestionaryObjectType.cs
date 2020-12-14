using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    // [OptionsValidationAttribute]
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
        
        [Required(ErrorMessage = "Поле обязательно5!")]
        public int CompanyId { get; set; }
        
        public string CompanyName { get; set; }
        
        [Display(Name = "Компания")]
        public List<ApplicationCompany> Companies { get; set; }

        //public List<int> SelectedPropertiesId { get; set; }
        
        [Display(Name = "Активный")]
        public bool IsUsed { get; set; }
        
        //[Display(Name = "Свойства")]
        //public List<ObjectProperty> ObjectProperties { get; set; } = new List<ObjectProperty>();

        // public class OptionsValidationAttribute : ValidationAttribute
        // {
        //     public override bool IsValid(object value)
        //     {
        //         QuestionaryObjectType opt = value as QuestionaryObjectType;
        //         if (opt != null && opt.SelectedPropertiesId == null && opt.NewSelectedObjectProperties == null)
        //         {
        //             ErrorMessage = "Не указаны свойства!";
        //             return false;
        //         }
        //         return true;
        //     }
        // }
    }
}
