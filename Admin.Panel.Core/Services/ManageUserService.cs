using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Interfaces.Repositories.Questionary;
using Admin.Panel.Core.Interfaces.Services;

namespace Admin.Panel.Core.Services
{
    public class ManageUserService: IManageUserService
    {
        private readonly ICompanyRepository _companyRepository;

        public ManageUserService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
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
    }
}
