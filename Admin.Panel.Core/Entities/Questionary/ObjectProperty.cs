using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    class ObjectProperty: BaseEntity
    {
        public string Name { get; set; }
        public string NameInReport { get; set; }
        public bool IsUsedInReport { get; set; }
        public string ReportCellStyle { get; set; }
    }
}
