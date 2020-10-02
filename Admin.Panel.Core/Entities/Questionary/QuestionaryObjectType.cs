using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    [OptionsValidationAttribute]
    public class QuestionaryObjectType: BaseEntity
    {
        [Required(ErrorMessage = "Поле Название - обязательно!")]
        [StringLength(250, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 2)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Display(Name = "Свойства")]
        //[Required(ErrorMessage = "Поле Свойства - обязательно!")]
        public List<ObjectProperty> SelectedObjectProperties { get; set; }
        
        [Display(Name = "Создать новое свойство")]
        public List<ObjectProperty> NewSelectedObjectProperties { get; set; }
        
        //[Required(ErrorMessage = "Поле Свойства - обязательно!")]
        public List<int> SelectedPropertiesId { get; set; }
        
        [Display(Name = "Активный")]
        public bool IsUsed { get; set; }
        
        [Display(Name = "Свойства")]
        public List<ObjectProperty> ObjectProperties { get; set; } = new List<ObjectProperty>();

        public class OptionsValidationAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                QuestionaryObjectType opt = value as QuestionaryObjectType;
                if (opt != null && opt.SelectedPropertiesId == null && opt.NewSelectedObjectProperties == null)
                {
                    this.ErrorMessage = "Не указаны свойства!";
                    return false;
                }
                return true;
            }
        }
    }
}
