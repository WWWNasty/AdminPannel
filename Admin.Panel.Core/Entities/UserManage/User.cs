using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Admin.Panel.Core.Entities.UserManage
{
    public class User : IdentityUser<int>
    {
        public string Nickname { get; set; }
        
        public bool IsConfirmed { get; set; }
        public string ConfirmationToken { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsUsed { get; set; }
        public List<int> ApplicationCompaniesId { get; set; }
        public int RoleId { get; set; }

        public string Description { get; set; }

        public List<string> RolesList { get; set; }

    }
}
