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

namespace Admin.Panel.Data.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("questionaryConnection");
        }


        public RegisterDto GetAllCompanies()
        {
            List<ApplicationCompany> companies = GetCompaniesAsync();
            RegisterDto registerObject = new RegisterDto
            {
                ApplicationCompanies = companies
            };

            return registerObject;
        }

        protected List<ApplicationCompany> GetCompaniesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    var query = "SELECT * FROM Companies";
                    var сompanies = connection.Query<ApplicationCompany>(query);
                    return сompanies.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
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
                        @"INSERT INTO ApplicationUser(UserName,PasswordHash,Nickname,CreatedDate,SecurityStamp,IsConfirmed,ConfirmationToken,IsUsed,RoleId) 
                            VALUES(@UserName,@PasswordHash,@Nickname,@CreatedDate,@SecurityStamp,@IsConfirmed,@ConfirmationToken,1,1);
                            SELECT UserId = @@IDENTITY";
                    var value = cn.ExecuteScalar<int>(query, user);

                    var userId = value;
                    var companyId = user.ApplicationCompanyId;

                    ApplicationUserCompany userCompany = new ApplicationUserCompany
                    {
                        UserId = userId,
                        CompanyId = companyId
                    };

                    var queryAddCompanyToUser = "INSERT INTO ApplicationUserCompany(UserId,CompanyId) VALUES(@UserId,@CompanyId)";

                    await cn.ExecuteAsync(queryAddCompanyToUser, userCompany);

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

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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

        public async Task<User> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Open();

                try
                {
                    var query = "SELECT * FROM ApplicationUser WHERE LOWER(UserName)=LOWER(@UserName)";
                    var user = await cn.QueryAsync<User>(query, new { @UserName = userName });

                    return user.SingleOrDefault();
                }
                catch (Exception ex)
                {

                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }

            }
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
           // cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSecurityStampAsync(User user, CancellationToken cancellationToken)
        {
            //cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.SecurityStamp);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            //cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetSecurityStampAsync(User user, string stamp, CancellationToken cancellationToken)
        {
           // cancellationToken.ThrowIfCancellationRequested();

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

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

    }
}
