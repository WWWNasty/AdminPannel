using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.Extensions.Configuration;
using Admin.Panel.Core.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using Admin.Panel.Core.Entities;
using System.Data.SqlClient;
using System.Linq;
using Admin.Panel.Core.Interfaces.Repositories;

namespace Admin.Panel.Data.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("questionaryConnection");
        }

        public Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync(cancellationToken);

                try
                {
                    var query =
                        @"INSERT INTO ApplicationUser(UserName,PasswordHash,Nickname,CreatedDate,SecurityStamp,IsConfirmed,ConfirmationToken,IsUsed,RoleId,NormalizedUserName,NormalizedEmail,Email,EmailConfirmed) 
                            VALUES(@UserName,@PasswordHash,@Nickname,@CreatedDate,@SecurityStamp,@IsConfirmed,@ConfirmationToken,1,1,@NormalizedUserName,@NormalizedEmail,@Email,@EmailConfirmed);
                            SELECT UserId = @@IDENTITY";
                    var value = cn.ExecuteScalar<int>(query, user);

                    if (user.ApplicationCompaniesId.Count != 0)
                    {
                        foreach (int company in user.ApplicationCompaniesId)
                        {
                            cn.Execute(
                                @"INSERT INTO ApplicationUserCompany(UserId,CompanyId) VALUES(@UserId,@CompanyId)",
                                new ApplicationUserCompany
                                {
                                    UserId = value,
                                    CompanyId = Convert.ToInt32(company)
                                });
                        }
                    }

                    // var companyId = user.;
                    //
                    // ApplicationUserCompany userCompany = new ApplicationUserCompany
                    // {
                    //     UserId = userId,
                    //     CompanyId = companyId
                    // };
                    //
                    // var queryAddCompanyToUser = "INSERT INTO ApplicationUserCompany(UserId,CompanyId) VALUES(@UserId,@CompanyId)";
                    //
                    // await cn.ExecuteAsync(queryAddCompanyToUser, userCompany);

                    return IdentityResult.Success;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection()", ex);
                }
            }
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);
                    return await connection.QuerySingleOrDefaultAsync<User>($@"SELECT * FROM [ApplicationUser]
                    WHERE [NormalizedEmail] = @{nameof(normalizedEmail)}", new {normalizedEmail});
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection()", ex);
                }
            }
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Open();

                try
                {
                    var query = "SELECT * FROM ApplicationUser WHERE Id=@Id";
                    var user = await cn.QueryAsync<User>(query, new { @Id = userId });

                    return user.SingleOrDefault();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Open();

                try
                {
                    //var query = "SELECT * FROM ApplicationUser WHERE LOWER(UserName)=LOWER(@UserName)";
                    var query = $@"SELECT * FROM [ApplicationUser]
                    WHERE [NormalizedUserName] = @{nameof(normalizedUserName)}";
                    var user = await cn.QueryAsync<User>(query, new { @NormalizedUserName = normalizedUserName});

                    return user.SingleOrDefault();
                }
                catch (Exception ex)
                {

                    throw new Exception($"{GetType().FullName}.наверное такой пользователь уже есть", ex);
                }

            }
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PasswordHash);
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();

                try
                {
                    var query = "SELECT r.[Name] FROM [ApplicationRole] r INNER JOIN [ApplicationUser] ur ON ur.[RoleId] = r.Id WHERE ur.Id = @userId";
                    var queryResults = await cn.QueryAsync<string>(query, new { userId = user.Id });

                    return queryResults.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public Task<string> GetSecurityStampAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.SecurityStamp);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var queryResults = await connection.QueryAsync<User>("SELECT u.* FROM [ApplicationUser] u " +
                                                                                "INNER JOIN [ApplicationUserRole] ur ON ur.[UserId] = u.[Id] INNER JOIN [ApplicationRole] r ON r.[Id] = ur.[RoleId] WHERE r.[NormalizedName] = @normalizedName",
                    new { normalizedName = roleName.ToUpper() });

                return queryResults.ToList();
            }
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
             using (var connection = new SqlConnection(_connectionString))
            {
                var roleId = await connection.ExecuteScalarAsync<int?>("SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (roleId == default(int)) return false;
                var matchingRoles = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM [ApplicationUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}",
                    new { userId = user.Id, roleId });
                
                return matchingRoles > 0;
            }
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetSecurityStampAsync(User user, string stamp, CancellationToken cancellationToken)
        {

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                await cn.OpenAsync();
                try
                {
                    await cn.ExecuteAsync(@"UPDATE ApplicationUser SET UserName=@UserName,PasswordHash=@PasswordHash,NickName=@NickName,SecurityStamp=@SecurityStamp,IsConfirmed=@IsConfirmed,
                    NormalizedUserName=@NormalizedUserName,NormalizedEmail=@NormalizedEmail,Email=@Email,
                    EmailConfirmed=@EmailConfirmed,IsUsed=@IsUsed WHERE Id=@Id", user);

                    return IdentityResult.Success;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public void Dispose()
        {

        }

        //public Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
