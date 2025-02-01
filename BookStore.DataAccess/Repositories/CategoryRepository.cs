using BookStore.DataAccess.Configurations;
using BookStore.Models.Entities;
using RepoSP.Net.Services;
using Microsoft.Data.SqlClient;
using System.Data;


namespace BookStore.DataAccess.Repositories
{
    /// <summary>
    /// Repository for managing Category entities using stored procedures.
    /// </summary>
    public class CategoryRepository : StoredProcedureRepository<Category>
    {
        private readonly string _connectionString;
        private readonly CategoriesStoredProcConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        public CategoryRepository(string connectionString)
            : base(connectionString, CategoriesStoredProcConfiguration.Instance)
        {
            _connectionString = connectionString;
            _config = CategoriesStoredProcConfiguration.Instance;
        }

        /// <summary>
        /// Gets a Category entity by its name asynchronously.
        /// </summary>
        /// <param name="categoryName">The name of the Category entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the Category entity if found; otherwise, null.</returns>
        public async Task<Category?> GetByNameAsync(string categoryName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetCategoryByName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", categoryName);

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
