using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;

namespace Admin.Panel.Core.Interfaces
{
    public interface IManageUserRepository
    {
        public Task<List<GetAllUsersDto>> GetAllUsers();
        public Task<List<GetAllUsersDto>> GetAllUsersForUser(int id);

        public Task<int> UpdateUser(UpdateUserViewModel user);

        public Task<User> GetUser(int userId);
    }
}
