using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;

namespace Admin.Panel.Core.Interfaces.Services
{
    public interface IManageUserService
    {
        public Task<RegisterDto> GetCompaniesAndRoles();
        Task<bool> IsUsed(string name, CancellationToken cancellationToken);
    }
}
