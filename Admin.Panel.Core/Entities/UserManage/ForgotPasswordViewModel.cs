using System.ComponentModel.DataAnnotations;

namespace Admin.Panel.Core.Entities.UserManage
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Введите email!")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
