using System;
using System.Collections.Generic;
using System.Text;
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

        public RegisterDto GetAllCompanies()
        {
            List<ApplicationCompany> companies = _companyRepository.GetAllAsync();
            RegisterDto registerObject = new RegisterDto
            {
                ApplicationCompanies = companies
            };

            return registerObject;
        }
    }
}
