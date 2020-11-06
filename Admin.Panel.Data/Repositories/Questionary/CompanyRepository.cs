using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Admin.Panel.Data.Repositories.Questionary
{
    public class CompanyRepository: ICompanyRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<CompanyRepository> _logger;

        public CompanyRepository(IConfiguration configuration, ILogger<CompanyRepository> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("questionaryConnection");
        }

        public async Task<ApplicationCompany> GetAsync(int companyId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    var query = "SELECT * FROM Companies WHERE CompanyId=@Id";
                    var сompany = await connection.QueryAsync<ApplicationCompany>(query, new { @Id = companyId });
                    return сompany.SingleOrDefault();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<ApplicationCompany>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    var query = "SELECT * FROM Companies";
                    var сompanies = await connection.QueryAsync<ApplicationCompany>(query);
                    return сompanies.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }
        
        public async Task<List<ApplicationCompany>> GetAllActiveAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    var query = "SELECT * FROM Companies c WHERE c.IsUsed = 1";
                    var сompanies = await connection.QueryAsync<ApplicationCompany>(query);
                    return сompanies.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<ApplicationCompany>> GetAllActiveForUserAsync(string userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var query = @"SELECT * FROM Companies c 
                                    INNER JOIN ApplicationUserCompany ac ON c.CompanyId = ac.CompanyId
                                        WHERE c.IsUsed = 1 AND ac.UserId =@UserId";
                    var сompanies = await connection.QueryAsync<ApplicationCompany>(query, new {@UserId = Convert.ToInt32(userId)});
                    return сompanies.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<ApplicationCompany> CreateAsync(ApplicationCompany company)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    var query = @"INSERT INTO Companies(CompanyName,CompanyDescription,IsUsed) 
                    VALUES(@CompanyName, @CompanyDescription,1)";
                    await connection.ExecuteAsync(query, company);
                    _logger.LogInformation("Компания {0} успешно добавлена в бд.", company.CompanyName);
                    return company;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<ApplicationCompany> UpdateAsync(ApplicationCompany company)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    var query = @"UPDATE Companies SET CompanyName=@CompanyName,CompanyDescription=@CompanyDescription,IsUsed=@IsUsed 
                     WHERE CompanyId=@CompanyId";
                    await connection.ExecuteAsync(query, company);
                    _logger.LogInformation("Компания с Id:{0} успешно отредактирована в бд.", company.CompanyId);
                    return company;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }
    }
}
