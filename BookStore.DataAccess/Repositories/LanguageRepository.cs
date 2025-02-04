using BookStore.DataAccess.Configurations;
using BookStore.Models.Entities;
using Microsoft.Data.SqlClient;
using RepoSP.Net.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repositories
{
    /// <summary>
    /// Repository for managing Language entities using stored procedures.
    /// </summary>
    public class LanguageRepository : StoredProcedureRepository<Language>
    {
        private readonly string _connectionString;
        private readonly LanguagesStoredProcConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        public LanguageRepository(string connectionString) : base(connectionString, LanguagesStoredProcConfiguration.Instance)
        {
            _connectionString = connectionString;
            _config = LanguagesStoredProcConfiguration.Instance;
        }

        // Not Implemented methods //

        /// <summary>
        /// Deletes a Language entity. This method is not supported in this repository and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="id">The ID of the Language entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override async Task<bool> Delete(int id)
        {
            throw new NotSupportedException("This method is not supported in this repository.");
        }

        /// <summary>
        /// Updates a Language entity. This method is not supported in this repository and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="entity">The Language entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override Task<bool> UpdateAsync(Language entity)
        {
            throw new NotSupportedException("This method is not supported in this repository.");
        }


        // Custom methods //

        /// <summary>
        /// Gets a Language entity by its name asynchronously.
        /// </summary>
        /// <param name="LanguageName">The name of the Language entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the Language entity if found; otherwise, null.</returns>
        public async Task<Language?> GetByNameAsync(string LanguageName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetByNameProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LanguageName", LanguageName);

                        connection.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                return _config.MapEntity(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
            }
            return null;
        }
    }
}
