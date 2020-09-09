using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    public class QuestionaryObject: BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime Updated { get; set; }
        public string Name { get; set; }
        public int ObjectTypeId { get; set; }
        public string ObjectTypeName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        
        //public List<ObjectProperty> SelectedObjectProperties { get; set; }
        public List<ObjectPropertyValues> SelectedObjectPropertyValues { get; set; }
        //public Dictionary<List<ObjectProperty>, List<ObjectPropertyValues>> PropertiesValues { get; set; }
      

        //create
        
        public List<QuestionaryObjectType> QuestionaryObjectTypes { get; set; }
        public List<ApplicationCompany> Companies { get; set; }

    }
}
