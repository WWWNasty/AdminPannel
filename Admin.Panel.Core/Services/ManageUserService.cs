using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Interfaces.Repositories;
using Admin.Panel.Core.Interfaces.Repositories.Questionary;
using Admin.Panel.Core.Interfaces.Services;

namespace Admin.Panel.Core.Services
{
    public class ManageUserService: IManageUserService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;

        public ManageUserService(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        public async Task<RegisterDto> GetAllCompanies()
        {
            List<ApplicationCompany> companies = await _companyRepository.GetAllAsync();
            RegisterDto registerObject = new RegisterDto
            {
                ApplicationCompanies = companies
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
