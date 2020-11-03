using System.Threading;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.UserManage;

namespace Admin.Panel.Core.Interfaces.Services.UserManageServiceInterfaces
{
    public interface IManageUserService
    {
        Task<RegisterDto> GetCompaniesAndRoles();
        Task<RegisterDto> GetCompaniesAndRolesForUser(string userId);
        Task<bool> IsUsed(string name, CancellationToken cancellationToken);
    }
}
