using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    public class QuestionaryObject: BaseEntity
    {
        [Required(ErrorMessage = "Поле обязательно!")]
        [StringLength(20, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 1)]
        [Display(Name = "Код")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Поле обязательно!")]
        [StringLength(250, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 1)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        
        [Required(ErrorMessage = "Поле обязательно!")]
        [StringLength(150, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 1)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Поле обязательно!")]
        public int ObjectTypeId { get; set; }
        
        [Display(Name = "Тип объекта")]
        public string ObjectTypeName { get; set; }

        [Display(Name = "Активный")]
        public bool IsUsed { get; set; }
        
        public string CompanyName { get; set; }

        public bool IsCodeUnique { get; set; } = true;

        [Required(ErrorMessage = "Поле обязательно!")]
        public List<ObjectPropertyValues> SelectedObjectPropertyValues { get; set; }
        
        [Display(Name = "Тип объекта")]
        public List<QuestionaryObjectType> QuestionaryObjectTypes { get; set; }

    }
}
