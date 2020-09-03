using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;

namespace Admin.Panel.Core.Interfaces.Repositories.Questionary
{
    public interface ICompanyRepository
    {
        public Task<ApplicationCompany> GetAsync(int id);
        public Task<List<ApplicationCompany>> GetAllAsync();
        public Task<ApplicationCompany> CreateAsync(ApplicationCompany company);
        public Task<ApplicationCompany> UpdateAsync(ApplicationCompany company);
        public Task Delete(ApplicationCompany company);

    }
}
