using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    public class ObjectPropertyValues: BaseEntity
    {
        public int QuestionaryObjectId { get; set; }
        public int ObjectPropertyId { get; set; }
        
        [Required(ErrorMessage = "Поле обязательно!")]
        [StringLength(250, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 1)]
        [Display(Name = "Значения")]
        public string Value { get; set; }
        
        public string Name { get; set; }
        // public string NameInReport { get; set; }
        // public bool IsUsedInReport { get; set; }
        // public string ReportCellStyle { get; set; }
    }
}
