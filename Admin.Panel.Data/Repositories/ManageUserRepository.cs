using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Admin.Panel.Data.Repositories
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

        //селать isused false пользователю
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
