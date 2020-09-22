using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Panel.Core.Entities;
using Admin.Panel.Core.Entities.Questionary;
using Admin.Panel.Core.Interfaces.Repositories.QuestionaryRepositoryInterfaces;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Admin.Panel.Data.Repositories.Questionary
{
    public class ObjectPropertiesRepository: IObjectPropertiesRepository
    {
        private readonly string _connectionString;

        public ObjectPropertiesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("questionaryConnection");
        }

        public async Task<ObjectProperty> GetAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    var query = "SELECT * FROM ObjectProperties WHERE Id=@Id";
                    var result = await connection.QueryAsync<ObjectProperty>(query, new { @Id = id });
                    return result.SingleOrDefault();
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<ObjectProperty>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var query = "SELECT * FROM ObjectProperties";
                    var result = connection.Query<ObjectProperty>(query).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<List<ObjectProperty>> GetAllActiveAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                try
                {
                    var query = "SELECT * FROM ObjectProperties WHERE IsUsed = 1";
                    var result = connection.Query<ObjectProperty>(query).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }
        
        public async Task<ObjectProperty> CreateAsync(ObjectProperty obj)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var query = @"INSERT INTO ObjectProperties(Name,NameInReport,IsUsedInReport,ReportCellStyle,IsUsed) 
                    VALUES(@Name,@NameInReport,@IsUsedInReport,@ReportCellStyle,1)";
                    await connection.ExecuteAsync(query, obj);
                    return obj;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }

        public async Task<ObjectProperty> UpdateAsync(ObjectProperty obj)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    var query = @"UPDATE ObjectProperties SET Name=@Name,NameInReport=@NameInReport,IsUsedInReport=@IsUsedInReport,ReportCellStyle=@ReportCellStyle,IsUsed=@IsUsed 
                     WHERE Id=@Id";
                    await connection.ExecuteAsync(query, obj);
                    return obj;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{GetType().FullName}.WithConnection__", ex);
                }
            }
        }
    }
}
