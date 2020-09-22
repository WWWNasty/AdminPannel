using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities.UserManage;
using Admin.Panel.Core.Interfaces;
using Admin.Panel.Core.Interfaces.Repositories.UserManageRepositoryInterfaces;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Admin.Panel.Data.Repositories.UserManage
{
    public class ManageUserRepository: IManageUserRepository
    {
        private readonly string _connectionString;

        public ManageUserRepository(IConfiguration configuration)
        {
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
                    }
                    return result.Distinct().ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<User> GetUser(int userId)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                try
                {
                    var query = "SELECT * FROM ApplicationUser WHERE Id=@Id";
                    var result = await cn.QueryAsync<User>(query, new { @Id = userId });

                    return result.SingleOrDefault();
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
                    await cn.ExecuteAsync(@"UPDATE ApplicationUser SET UserName=@UserName,NickName=@NickName,
                    Email=@Email,IsUsed=@IsUsed WHERE Id=@Id", user);

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
