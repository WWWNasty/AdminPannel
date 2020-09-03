using System;
using System.Collections.Generic;
using System.Text;
using Admin.Panel.Core.Entities;

namespace Admin.Panel.Core.Interfaces.Services
{
    public interface IManageUserService
    {
        public RegisterDto GetAllCompanies();
    }
}
