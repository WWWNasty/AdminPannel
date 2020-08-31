using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.Panel.Core.Entities
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Введите email!")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
