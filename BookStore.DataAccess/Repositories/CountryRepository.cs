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

        public async Task<Country?> GetCountryByNameAsync(string Name)
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

        /// <summary>
        /// Deletes a Country entity. This method is not supported in this repository and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="id">The ID of the Country entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override async Task<bool> Delete(int id)
        {
            throw new NotSupportedException("This method is not supported in this repository.");
        }


        /// <summary>
        /// Insert a Country entity. This method is not supported in this repository and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="entity">The Entity of the Country entity to Add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override Task<Country> InsertAsync(Country entity)
        {
            throw new NotSupportedException("This method is not supported in this repository.");
        }

        /// <summary>
        /// Update a Country entity. This method is not supported in this repository and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="entity">The Entity of the Country entity to Update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override Task<bool> UpdateAsync(Country entity)
        {
            throw new NotSupportedException("This method is not supported in this repository.");
        }

    }
}
