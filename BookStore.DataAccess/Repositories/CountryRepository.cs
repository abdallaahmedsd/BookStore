using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using RepoSP.Net.Services;
using BookStore.DataAccess.Configurations;
using BookStore.Models.Entities;

namespace BookStore.DataAccess.Repositories
{
    public class CountryRepository : StoredProcedureRepository<Country>
    {
        private readonly string _connectionString;
        private readonly CountriesStoredProcConfiguration _config;
        public CountryRepository(string ConnectionString) : base(ConnectionString, CountriesStoredProcConfiguration.Instance)
        {
            this._connectionString = ConnectionString;
            _config = CountriesStoredProcConfiguration.Instance;
        }

        public async Task<Country> GetCountryByNameAsync(string Name)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.SP_GetCountryByName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", Name);

                       

                        connection.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                                return _config.MapEntity(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle exception as needed
            }
            return null;
        }
    }
}
