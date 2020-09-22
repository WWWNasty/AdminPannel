using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.UserManage;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Repositories.UserManageRepositoryInterfaces;
using Admin.Panel.Core.Interfaces.Services.UserManageServiceInterfaces;

namespace Admin.Panel.Core.Services.UserManageServices
{
    public class ManageUserService: IManageUserService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public ManageUserService(ICompanyRepository companyRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        
        
        public async Task<RegisterDto> GetCompaniesAndRoles()
        {
            List<ApplicationCompany> companies = await _companyRepository.GetAllActiveAsync();
            List<ApplicationRole> roles = await _roleRepository.GetAllRolesAsync();
            
            RegisterDto registerObject = new RegisterDto
            {
                ApplicationCompanies = companies,
                RolesList = roles
            };

            return registerObject;
        }

        public async Task<RegisterDto> GetCompaniesAndRolesForUser(string userId)
        {
            List<ApplicationCompany> companies = await _companyRepository.GetAllActiveForUserAsync(userId);
            List<ApplicationRole> roles = await _roleRepository.GetAllRolesAsyncButSuperAdmin();
            
            RegisterDto registerObject = new RegisterDto
            {
                ApplicationCompanies = companies,
                RolesList = roles
            };

            return registerObject;
        }
        
        public async Task<bool> IsUsed(string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await _userRepository.FindByNameAsync(name.ToUpper(), cancellationToken);
            if (result != null)
            {
                if (result.IsUsed == false)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

     
    }
}
