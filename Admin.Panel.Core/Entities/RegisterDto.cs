using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;

namespace Admin.Panel.Core.Entities
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Поле Email - обязательно!")]
        [EmailAddress(ErrorMessage = "Некорректный электронный адрес.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле ФИО пользователя - обязательно!")]
        [StringLength(250, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 4)]
        [Display(Name = "ФИО пользователя")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Поле Пароль - обязательно!")]
        [StringLength(100, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Поле Компания - обязательно!")]
        [Display(Name = "Компания")]
        public int CompanyId { get; set; }

        //public string CompanyName { get; set; }
        public string ConfirmationToken { get; set; }

        public List<ApplicationCompany> ApplicationCompanies { get; set; } = new List<ApplicationCompany>();
    }
}
