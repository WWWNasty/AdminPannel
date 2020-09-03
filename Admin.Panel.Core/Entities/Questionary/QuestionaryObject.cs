using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    class QuestionaryObject: BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime Updated { get; set; }
        public string Name { get; set; }
        public int ObjectTypeId { get; set; }
        public int CompanyId { get; set; }
    }
}
