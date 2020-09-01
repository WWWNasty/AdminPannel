using System.Threading;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Admin.Panel.Core.Interfaces
{
    public interface IUserRepository : IUserStore<User>, IUserLoginStore<User>, IUserPasswordStore<User>, IUserSecurityStampStore<User>, IUserRoleStore<User>, IUserEmailStore<User>
    {
        public RegisterDto GetAllCompanies();
        public Task<bool> IsUsed(string name, CancellationToken cancellationToken);
    }
}