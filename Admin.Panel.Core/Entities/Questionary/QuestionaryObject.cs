using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    public class QuestionaryObject: BaseEntity
    {
        [Required(ErrorMessage = "Поле Код - обязательно!")]
        [StringLength(20, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 1)]
        [Display(Name = "Код")]
        public string Code { get; set; }
        
        [Required(ErrorMessage = "Поле Описание - обязательно!")]
        [StringLength(250, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 2)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        
        [Required(ErrorMessage = "Поле Название - обязательно!")]
        [StringLength(150, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 2)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Поле Тип объекта - обязательно!")]
        public int ObjectTypeId { get; set; }
        
        [Display(Name = "Тип объекта")]
        public string ObjectTypeName { get; set; }
        
        [Required(ErrorMessage = "Поле Компания - обязательно!")]
        public int CompanyId { get; set; }
        
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Значения - обязательны!")]
        public List<ObjectPropertyValues> SelectedObjectPropertyValues { get; set; }

        //create
        [Display(Name = "Тип объекта")]
        public List<QuestionaryObjectType> QuestionaryObjectTypes { get; set; }
        
        [Display(Name = "Компания")]
        public List<ApplicationCompany> Companies { get; set; }

    }
}
