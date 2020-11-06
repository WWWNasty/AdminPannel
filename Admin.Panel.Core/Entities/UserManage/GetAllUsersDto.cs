using System;
using System.Collections.Generic;

namespace Admin.Panel.Core.Entities.UserManage
{
    public class GetAllUsersDto
    {
        protected bool Equals(GetAllUsersDto other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GetAllUsersDto) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public int Id { get; set; }

        public string UserName { get; set; }
        
        public string Email { get; set; }
       
        public string Nickname { get; set; }
        
        public string SecurityStamp { get; set; }
     
        public DateTime CreatedDate { get; set; }
        public bool IsUsed { get; set; }
        public string Role { get; set; }
        public List<ApplicationCompany> Companies { get; set; }
        
        public int ApplicationCompanyId { get; set; }
    }
}
