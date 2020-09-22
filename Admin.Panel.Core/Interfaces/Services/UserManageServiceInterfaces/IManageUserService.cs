using System.Threading;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.UserManage;

namespace Admin.Panel.Core.Interfaces.Services.UserManageServiceInterfaces
{
    public interface IManageUserService
    {
        public Task<RegisterDto> GetCompaniesAndRoles();
        public Task<RegisterDto> GetCompaniesAndRolesForUser(string userId);
        Task<bool> IsUsed(string name, CancellationToken cancellationToken);
    }
}
