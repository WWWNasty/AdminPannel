using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.UserManage;
using Admin.Panel.Core.Interfaces;
using Admin.Panel.Core.Interfaces.Repositories.UserManageRepositoryInterfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Admin.Panel.Data.Repositories.UserManage
{
    public class ManageUserRepository: IManageUserRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<ManageUserRepository> _logger;

        public ManageUserRepository(IConfiguration configuration, ILogger<ManageUserRepository> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("questionaryConnection");
        }


        public async Task<List<GetAllUsersDto>> GetAllUsers()
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                try
                {
                    var query = "SELECT * FROM ApplicationUser";
                    var result = await cn.QueryAsync<GetAllUsersDto>(query);
                    foreach (var user in result)
                    {
                        List<ApplicationCompany> companies =  cn.Query<ApplicationCompany>(@"SELECT c.* FROM Companies c 
                        INNER JOIN ApplicationUserCompany ac ON ac.CompanyId = c.CompanyId
                        WHERE ac.UserId = @UserId", new {@UserId = user.Id}).ToList();
                        user.Companies = companies;

                        string role = cn.Query<string>(@"SELECT r.Name FROM ApplicationRole r
                        INNER JOIN ApplicationUser u ON u.RoleId = r.Id
                        WHERE u.Id = @UserId", new {@UserId = user.Id}).FirstOrDefault();
                        user.Role = role;
                    }
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<GetAllUsersDto>> GetAllUsersForUser(int idUser)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                try
                {
                    List<int> companiesId =  cn.Query<int>(@"SELECT c.CompanyId FROM ApplicationUserCompany c WHERE UserID = @UserId", new {@UserId = idUser}).ToList();

                    List<GetAllUsersDto> result = new List<GetAllUsersDto>();
                    foreach (var companyId in companiesId)
                    {
                        var users = cn.Query<GetAllUsersDto>(@"SELECT u.* FROM ApplicationUser u
                                                                                        INNER JOIN ApplicationUserCompany ac ON ac.UserId = u.Id
                                                                                        WHERE CompanyId = @CompanyId", new{@CompanyId = companyId}).ToArray();
                        foreach (var usr in users)
                        {
                            result.Add(usr);
                        }
                        foreach (var user in result)
                        {
                            List<ApplicationCompany> companies =  cn.Query<ApplicationCompany>(@"SELECT c.* FROM Companies c 
                        INNER JOIN ApplicationUserCompany ac ON ac.CompanyId = c.CompanyId
                        WHERE ac.UserId = @UserId", new {@UserId = user.Id}).ToList();
                            user.Companies = companies;

                            string role = cn.Query<string>(@"SELECT r.Name FROM ApplicationRole r
                        INNER JOIN ApplicationUser u ON u.RoleId = r.Id
                        WHERE u.Id = @UserId", new {@UserId = user.Id}).FirstOrDefault();
                            user.Role = role;
                        }
                    }
                    return result.Distinct().ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<UpdateUserViewModel> GetUser(int userId)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                try
                {
                    var query = @"SELECT u.*, r.Name AS Role FROM ApplicationUser u
                    Inner Join ApplicationRole r on r.Id = u.RoleId
                    WHERE u.Id = @Id";
                    var result = cn.Query<UpdateUserViewModel>(query, new { @Id = userId }).SingleOrDefault();
                    
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<bool> IsAdminLastActive()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    List<UpdateUserViewModel> users =  connection.Query<UpdateUserViewModel>(@"SELECT * FROM ApplicationUser u 
                                                                                                        INNER JOIN ApplicationRole r ON r.Id = u.RoleId
                                                                                                            WHERE r.Name = 'SuperAdministrator' AND u.IsUsed = 1").ToList();
                    if (users.Count <= 1)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        //делает isused false пользователю
        public async Task<int> UpdateUser(UpdateUserViewModel user)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();
                try
                {
                    await cn.ExecuteAsync(@"UPDATE ApplicationUser SET IsUsed=@IsUsed WHERE Id=@Id", user);
                    _logger.LogInformation("Пользователь с Id: {0} успешно отредактирован в бд.", user.Id);
                    return user.Id;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }
    }
}
