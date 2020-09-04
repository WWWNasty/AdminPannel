using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    public class ObjectPropertyToObjectTypes: BaseEntity
    {
        public int QuestionaryObjectTypeId { get; set; }
        public int ObjectPropertyId { get; set; }
    }
}
