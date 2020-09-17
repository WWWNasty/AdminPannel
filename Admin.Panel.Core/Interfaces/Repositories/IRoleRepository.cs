using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Admin.Panel.Core.Interfaces.Repositories
{
    public interface IRoleRepository: IRoleStore<ApplicationRole>
    {
        public Task<List<ApplicationRole>> GetAllRolesAsync();
        public Task<List<ApplicationRole>> GetAllRolesAsyncButSuperAdmin();
    }
}