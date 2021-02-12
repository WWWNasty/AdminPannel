using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.Panel.Core.Entities.UserManage
{
    public class UpdateUserViewModel
    {
        public int Id { get; set; }
        public bool IsUsed { get; set; }
        public string UserName { get; set; }
        
        public string Email { get; set; }

        public string Nickname { get; set; }
        
        public string Role { get; set; }

        [Required(ErrorMessage = "Поле Компания - обязательно!")]
        public List<int> SelectedCompaniesId { get; set; }
        
        [Required(ErrorMessage = "Поле Роль - обязательно!")]
        public int RoleId { get; set; }
        
        public bool IsAdminLastActive { get; set; }
        
        public string Description { get; set; }
        
        [Display(Name = "Роль")]
        public List<ApplicationRole> RolesList { get; set; }

        [Display(Name = "Компания")]
        public List<ApplicationCompany> ApplicationCompanies { get; set; } = new List<ApplicationCompany>();
    }
}
