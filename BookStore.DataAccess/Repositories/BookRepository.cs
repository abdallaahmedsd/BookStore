using BookStore.DataAccess.Configurations;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels;
using RepoSP.Net.Services;
using Microsoft.Data.SqlClient;
using System.Data;


namespace BookStore.DataAccess.Repositories
{
    /// <summary>
    /// Repository for managing Book entities using stored procedures.
    /// </summary>
    public class BookRepository : StoredProcedureRepository<Book>
    {
        private readonly string _connectionString;
        private readonly BooksStoredProcConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        public BookRepository(string connectionString) : base(connectionString, BooksStoredProcConfiguration.Instance)
        {
            _connectionString = connectionString;
            _config = BooksStoredProcConfiguration.Instance;
        }

        /// <summary>
        /// Gets a Book entity by its title asynchronously.
        /// </summary>
        /// <param name="Title">The title of the Book entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the Book entity if found; otherwise, null.</returns>
        public async Task<Book?> GetByTitleAsync(string Title)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetBookByTitle, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Title", Title);
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
                // Log the exception or handle it as needed
            }
            return null;
        }

        /// <summary>
        /// Gets a Book entity by its ISBA asynchronously.
        /// </summary>
        /// <param name="ISBA">The ISBA of the Book entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the Book entity if found; otherwise, null.</returns>
        public async Task<Book?> GetByISBAAsync(string ISBA)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetBookByISBA, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ISBA", ISBA);
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

        /// <summary>
        /// Gets all Book entities by author ID asynchronously.
        /// </summary>
        /// <param name="authorId">The ID of the author associated with the Book entities.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of Book entities.</returns>
        public async Task<IEnumerable<Book>> GetAllByAuthorAsync(int authorId)
        {
            var listOfBooks = new List<Book>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetBooksByAuthor, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AuthorID", authorId);

                        connection.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                listOfBooks.Add(_config.MapEntity(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
            }
            return listOfBooks;
        }

        /// <summary>
        /// Gets all Book entities by category ID asynchronously.
        /// </summary>
        /// <param name="categoryId">The ID of the category associated with the Book entities.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of Book entities.</returns>
        public async Task<IEnumerable<Book>> GetAllByCategoryAsync(int categoryId)
        {
            List<Book> books = new List<Book>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetBooksByCategoryID, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CategoryID", categoryId);
                        connection.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                books.Add(_config.MapEntity(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
            }

            return books;
        }

        public async Task<IEnumerable<BestSellingBookDTO>> GetTopBestSellingBooksAsync(int topN)
        {
            var bestSellingBooks = new List<BestSellingBookDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetTopBestSellingBooksDetails, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TopN", topN);

                        connection.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var book = new BestSellingBookDTO()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Title = reader.GetString(reader.GetOrdinal("Title")),
                                    AuthorName = reader.GetString(reader.GetOrdinal("AuthorName")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                    CoverImage = reader["CoverImage"] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("CoverImage")),
                                    TotalQuantity = reader.GetInt32(reader.GetOrdinal("TotalQuantity"))
                                };
                                bestSellingBooks.Add(book);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return bestSellingBooks;
        }

        /// <summary>
        /// Retrieves the top N recently published books from the database.
        /// </summary>
        /// <param name="topN">The number of top recently published books to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of recently published books.</returns>
        public async Task<IEnumerable<RecentlyPublishedBookDTO>> GetTopRecentlyPublishedBooksAsync(int topN)
        {
            var recentlyPublishedBooks = new List<RecentlyPublishedBookDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetTopRecentlyPublishedBooks, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TopN", topN);

                        connection.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var book = new RecentlyPublishedBookDTO
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Title = reader.GetString(reader.GetOrdinal("Title")),
                                    AuthorName = reader.GetString(reader.GetOrdinal("AuthorName")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                    CoverImage = reader["CoverImage"] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("CoverImage")),
                                    PublicationDate = reader.GetDateTime(reader.GetOrdinal("PublicationDate"))
                                };
                                recentlyPublishedBooks.Add(book);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return recentlyPublishedBooks;
        }

    }

}

