using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Panel.Core.Entities
{
    public class ConfirmationToken
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
