using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    public class QuestionaryObjectType: BaseEntity
    {
        [Required(ErrorMessage = "Поле Название - обязательно!")]
        [StringLength(250, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 2)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Display(Name = "Свойства")]
        public List<ObjectProperty> SelectedObjectProperties { get; set; }
        
        [Required(ErrorMessage = "Поле Свойства - обязательно!")]
        public List<int> SelectedPropertiesId { get; set; }
        
        [Display(Name = "Свойства")]
        public List<ObjectProperty> ObjectProperties { get; set; } = new List<ObjectProperty>();

    }
}
