﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    public class ObjectProperty: BaseEntity
    {
        public string Name { get; set; }
        public string NameInReport { get; set; }
        public bool IsUsedInReport { get; set; }
        public string ReportCellStyle { get; set; }

        public List<ObjectProperty> ObjectProperties { get; set; }
        public string Value { get; set; }

    }
}
