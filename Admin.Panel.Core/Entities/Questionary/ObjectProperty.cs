using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    public class ObjectProperty: BaseEntity
    {
        [Required(ErrorMessage = "Поле Название - обязательно!")]
        [StringLength(250, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 2)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Поле Название в отчете - обязательно!")]
        [StringLength(250, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 2)]
        [Display(Name = "Название в отчете")]
        public string NameInReport { get; set; }
        
        [Display(Name = "Используется в отчете")]
        public bool IsUsedInReport { get; set; }

        [Display(Name = "Активно")]
        public bool IsUsed { get; set; }
        
        [Required(ErrorMessage = "Поле Тип поля в отчете - обязательно!")]
        [StringLength(250, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 3)]
        [Display(Name = "Тип поля в отчете")]
        public string ReportCellStyle { get; set; }
        
        public List<ObjectProperty> ObjectProperties { get; set; }
        
        //public string Value { get; set; }

    }
}
