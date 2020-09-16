using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.Panel.Core.Entities
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Поле Предидущий пароль - обязательно!")]
        [DataType(DataType.Password)]
        [Display(Name = "Предидущий пароль")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Поле Новый пароль - обязательно!")]
        [StringLength(100, ErrorMessage = "Длина {0} должна быть не менее {2} символов и не более {1} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите новый пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароль не совпадает с новым паролем!")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
