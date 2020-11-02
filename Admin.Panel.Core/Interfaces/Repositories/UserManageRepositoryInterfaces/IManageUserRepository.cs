using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.UserManage;

namespace Admin.Panel.Core.Interfaces.Repositories.UserManageRepositoryInterfaces
{
    public interface IManageUserRepository
    {
        public Task<List<GetAllUsersDto>> GetAllUsers();
        public Task<List<GetAllUsersDto>> GetAllUsersForUser(int id);

        public Task<int> UpdateUser(UpdateUserViewModel user);

        public Task<UpdateUserViewModel> GetUser(int userId);

        public Task<bool> IsAdminLastActive();
    }
}
