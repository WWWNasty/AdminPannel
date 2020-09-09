using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    public class ObjectPropertyValues: BaseEntity
    {
        public int QuestionaryObjectId { get; set; }
        public int ObjectPropertyId { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public string NameInReport { get; set; }
        public bool IsUsedInReport { get; set; }
        public string ReportCellStyle { get; set; }
    }
}
