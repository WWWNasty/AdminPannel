using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Panel.Core.Entities
{
    public class GetAllUsersDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        //public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        //public bool EmailConfirmed { get; set; }
        //public string NormalizedEmail { get; set; }
        public string Nickname { get; set; }
        //public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        //public bool IsConfirmed { get; set; }
        //public string ConfirmationToken { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsUsed { get; set; }
        public int ApplicationCompanyId { get; set; }
    }
}
