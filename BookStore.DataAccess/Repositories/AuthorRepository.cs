using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreBackend.DAL.Configurations;
using RepoSP.Net.Services;
using BookStore.Models.Entities;
using BookStore.DataAccess.Configurations;
using BookStore.Models.ViewModels.Admin;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BookStore.DataAccess.Repositories
{
    public class AuthorRepository : StoredProcedureRepository<Author>
    {

        private readonly string _connectionString;
        private readonly AuthorsStoredProcConfiguration _config;
        public AuthorRepository(string connectionString) : base(connectionString, AuthorsStoredProcConfiguration.Instance)
        {
            _connectionString = connectionString;
            _config = AuthorsStoredProcConfiguration.Instance;
        }

        public async Task<IEnumerable<AuthorViewModel>> GetAllAuthorsId_Name()
        {
            var results = new List<AuthorViewModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetAllAuthorsId_Name, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                AuthorViewModel viewModel = new AuthorViewModel { Id = reader.GetInt32(reader.GetOrdinal("Id")), Name = reader.GetString(reader.GetOrdinal("FullName")) };
                                results.Add(viewModel);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
            }
            return results;
        }
    }
}
