using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.UserManage;
using Microsoft.AspNetCore.Identity;

namespace Admin.Panel.Core.Interfaces.Repositories.UserManageRepositoryInterfaces
{
    public interface IUserRepository : IUserStore<User>, IUserLoginStore<User>, IUserPasswordStore<User>, IUserSecurityStampStore<User>, IUserRoleStore<User>, IUserEmailStore<User> /*IUserLockoutStore<User>*/
    {
        string IsUserInRoleAsync(int idUser);
        int GetIdByName(string name);
    }
}