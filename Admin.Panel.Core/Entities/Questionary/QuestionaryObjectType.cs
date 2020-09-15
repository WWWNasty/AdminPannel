using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary
{
    public class QuestionaryObjectType: BaseEntity
    {
        public string Name { get; set; }
        public List<ObjectProperty> SelectedObjectProperties { get; set; }
        public List<int> SelectedPropertiesId { get; set; }
        public List<ObjectProperty> ObjectProperties { get; set; } = new List<ObjectProperty>();

    }
}
