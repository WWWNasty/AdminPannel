using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.Panel.Core.Entities.Questionary.Completed
{
    [QueryParametersValidationAttribute]
    public class QueryParameters
    {
        public List<CompletedQuestionary> CompletedQuestionaries { get; set; }

        public List<ApplicationCompany> ApplicationCompanies { get; set; }
        public List<QuestionaryObjectType> QuestionaryObjectTypes { get; set; }
        public List<QuestionaryObject> QuestionaryObjects { get; set; }

        public int[] CompanyIds { get; set; }
        public int[] ObjectTypeIds { get; set; }
        public int[] ObjectIds { get; set; }

        public int PageNumber { get; set; } = 1; // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        // public int TotalPages  // всего страниц
        // {
        //     get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        // }

        public class QueryParametersValidationAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                QueryParameters opt = value as QueryParameters;
                if (opt != null && opt.CompanyIds == null && opt.ObjectIds == null && opt.ObjectTypeIds == null)
                {
                    ErrorMessage = "Установите условие фильтра!";
                    return false;
                }
                return true;
            }
        }
    }
}