using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;

namespace Admin.Panel.Core.Interfaces.Repositories.Questionary
{
    public interface ICompanyRepository
    {
        public Task<ApplicationCompany> GetAsync();
        public List<ApplicationCompany> GetAllAsync();
        public Task<ApplicationCompany> UpdateAsync();
        public Task<ApplicationCompany> Delete();
    }
}
