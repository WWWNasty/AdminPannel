using System;
using System.Collections.Generic;

namespace Admin.Panel.Core.Entities.Questionary.Completed
{
    public class CompletedQuestionary: BaseEntity
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int ObjectTypeId { get; set; }
        public string ObjectType { get; set; }
        public int ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string PhoneNumber { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Comment { get; set; }

    }
}