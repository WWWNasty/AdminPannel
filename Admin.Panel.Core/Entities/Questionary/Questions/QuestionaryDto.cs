using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.Panel.Core.Entities.Questionary.Questions
{
    public class QuestionaryDto: BaseEntity
    {
        [Required(ErrorMessage = "Поле Название - обязательно!")]
        [StringLength(250, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 1)]
        [Display(Name = "Название Анкеты")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Поле Тип Объекта - обязательно!")]
        public int ObjectTypeId { get; set; }
        
        [Display(Name = "Тип Объекта")]
        public string ObjectTypeName { get; set; }
        
        [Required(ErrorMessage = "Поле Компания - обязательно!")]
        public int CompanyId { get; set; }

        [Display(Name = "Активна анкета")]
        public bool IsUsed { get; set; }

        [Display(Name = "Компания")]
        public string CompanyName { get; set; }
        
        [Required(ErrorMessage = "Не указаны вопросы!")]
        public List<QuestionaryQuestions> QuestionaryQuestions { get; set; } = new List<QuestionaryQuestions>();

        [Display(Name = "Компания")]
        public List<ApplicationCompany> ApplicationCompanies { get; set; }
        
        [Display(Name = "Тип ввода")]
        public List<QuestionaryInputFieldTypes> QuestionaryInputFieldTypes { get; set; }
        
        [Display(Name = "Тип списка ответов")]
        public List<SelectableAnswersLists> SelectableAnswersLists { get; set; }
        
        [Display(Name = "Тип Объекта")]
        public List<QuestionaryObjectType> QuestionaryObjectTypes { get; set; }
        
        [Display(Name = "Ответ по умолчанию")]
        public List<SelectableAnswers> SelectableAnswers { get; set; }
        public List<QuestionaryObject> QuestionaryObjects { get; set; }
        public List<ObjectProperty> ObjectProperties { get; set; }
        //public List<SelectableAnswers> SelectableAnswerse { get; set; }
        public int IndexCurrentQuestion { get; set; }
        public bool IfQuestionaryCurrentInCompany { get; set; }

    }
}