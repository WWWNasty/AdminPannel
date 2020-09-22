using System.ComponentModel.DataAnnotations;

namespace Admin.Panel.Core.Entities.UserManage
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Введите email!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле Пароль - обязательно!")]
        [StringLength(100, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
