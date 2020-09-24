using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.Panel.Core.Entities.Questionary.Questions
{
    public class QuestionaryDto: BaseEntity
    {
        [Required(ErrorMessage = "Поле Название - обязательно!")]
        [StringLength(250, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 2)]
        [Display(Name = "Название Анкеты")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Поле Тип Объекта - обязательно!")]
        public int ObjectTypeId { get; set; }
        
        [Display(Name = "Тип Объекта")]
        public string ObjectTypeName { get; set; }
        
        [Required(ErrorMessage = "Поле Компания - обязательно!")]
        public int CompanyId { get; set; }

        [Display(Name = "Компания")]
        public string CompanyName { get; set; }
        
        [Required(ErrorMessage = "Не указаны вопросы!")]
        public List<QuestionaryQuestions> QuestionaryQuestions { get; set; }
        

        [Display(Name = "Компания")]
        public List<ApplicationCompany> ApplicationCompanies { get; set; }
        
        [Display(Name = "Тип ввода")]
        public List<QuestionaryInputFieldTypes> QuestionaryInputFieldTypes { get; set; }
        
        [Display(Name = "Тип списка ответов")]
        public List<SelectableAnswersLists> SelectableAnswersLists { get; set; }
        
        [Display(Name = "Тип Объекта")]
        public List<QuestionaryObjectType> QuestionaryObjectTypes { get; set; }
    }
}