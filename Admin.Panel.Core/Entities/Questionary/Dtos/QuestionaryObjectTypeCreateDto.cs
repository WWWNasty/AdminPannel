using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Panel.Core.Entities.Questionary.Dtos
{
    public class QuestionaryObjectTypeCreateDto: BaseEntity
    {
        public string Name { get; set; }
        public List<ObjectProperty> ObjectProperties { get; set; }
    }
}
