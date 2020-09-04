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
    }
}
