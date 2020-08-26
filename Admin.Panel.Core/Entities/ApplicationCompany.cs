using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Panel.Core.Entities
{
    public class ApplicationCompany : BaseEntity
    { 
        public int CompanyId
        {
            get => Id;
            set => Id = value;
        }
        public string CompanyName { get; set; }
    }
}