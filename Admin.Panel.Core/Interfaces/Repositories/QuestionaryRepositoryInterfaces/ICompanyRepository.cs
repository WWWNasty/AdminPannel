using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;

namespace Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces
{
    public interface ICompanyRepository
    {
        public Task<ApplicationCompany> GetAsync(int id);
        public Task<List<ApplicationCompany>> GetAllAsync();
        public Task<List<ApplicationCompany>> GetAllActiveAsync();
        public Task<List<ApplicationCompany>> GetAllActiveForUserAsync(string userId);
        public Task<ApplicationCompany> CreateAsync(ApplicationCompany company);
        public Task<ApplicationCompany> UpdateAsync(ApplicationCompany company);
        //public Task DeleteAsync(ApplicationCompany company);

    }
}
