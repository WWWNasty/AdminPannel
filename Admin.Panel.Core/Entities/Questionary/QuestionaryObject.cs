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
        public int CompanyId { get; set; }
        //TODO create model нужна
        public string Value { get; set; }

    }
}
