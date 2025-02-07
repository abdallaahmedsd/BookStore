using BookStore.DataAccess.Configurations;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels;
using RepoSP.Net.Services;
using Microsoft.Data.SqlClient;
using System.Data;
using BookStore.Models.ViewModels.Customer.Book;
using BookStore.Models.ViewModels.Book;


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
        public async Task<Book?> GetByISBNAsync(string ISBA)
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

        /// <summary>
        /// Retrieves the details of a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <returns>A <see cref="BookDetailsViewModel"/> instance if found; otherwise, null.</returns>
        public async Task<BookDetailsViewModel?> GetBookDetailsByIdAsync(int id)
        {
            BookDetailsViewModel? bookDetails = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(_config.GetBookDetailsById, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter($"@{_config.IdParameterName}", id));

                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            bookDetails = new BookDetailsViewModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                ISBA = reader.GetString(reader.GetOrdinal("ISBA")),
                                PublicationDate = reader.GetDateTime(reader.GetOrdinal("PublicationDate")),
                                CoverImage = reader.IsDBNull(reader.GetOrdinal("CoverImage")) ? null : reader.GetString(reader.GetOrdinal("CoverImage")),
                                LanguageName = reader.GetString(reader.GetOrdinal("LanguageName")),
                                AuthorName = reader.GetString(reader.GetOrdinal("AuthorName")),
                                TotalSellingQuantity = reader.GetInt32(reader.GetOrdinal("TotalSellingQuantity"))
                            };
                        }
                    }
                }
            }

            return bookDetails;
        }

        /// <summary>
        /// Retrieves the list of books for admin.
        /// </summary>
        /// <returns>A list of <see cref="BookListViewModel"/> instances.</returns>
        public async Task<List<BookListViewModel>> GetBookListAsync()
        {
            var results = new List<BookListViewModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SELECT * FROM Books.v_BooksSummaryList", connection))
                {
                    command.CommandType = CommandType.Text;

                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var book = new BookListViewModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                AuthorName = reader.GetString(reader.GetOrdinal("AuthorName")),
                                CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                CoverImage = reader.IsDBNull(reader.GetOrdinal("CoverImage")) ? null : reader.GetString(reader.GetOrdinal("CoverImage"))
                            };
                            results.Add(book);
                        }
                    }
                }
            }

            return results;
        }


        /// <summary>
        /// Retrieves the top N best-selling books.
        /// </summary>
        /// <param name="topN">The number of top best-selling books to retrieve.</param>
        /// <returns>A IEnumerable of <see cref="BookHomeBestSellingViewModel"/> instances.</returns>
        public async Task<IEnumerable<BookHomeBestSellingViewModel>> GetTopBestSellingBooksAsync(int topN)
        {
            var results = new List<BookHomeBestSellingViewModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(_config.GetTopBestSellingBooks, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@TopN", topN));

                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var book = new BookHomeBestSellingViewModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                AuthorName = reader.GetString(reader.GetOrdinal("AuthorName")),
                                CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                CoverImage = reader.IsDBNull(reader.GetOrdinal("CoverImage")) ? null : reader.GetString(reader.GetOrdinal("CoverImage")),
                                TotalSellingQuantity = reader.GetInt32(reader.GetOrdinal("TotalSellingQuantity")).ToString()
                            };
                            results.Add(book);
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Retrieves the last added books.
        /// </summary>
        /// <param name="topN">The number of most recently added books to retrieve.</param>
        /// <returns>A list of <see cref="BookHomeLastAddedViewModel"/> instances.</returns>
        public async Task<IEnumerable<BookHomeLastAddedViewModel>> GetLastAddedBooksAsync(int topN)
        {
            var results = new List<BookHomeLastAddedViewModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(_config.GetLastAddedBooks, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@TopN", topN));

                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var book = new BookHomeLastAddedViewModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                AuthorName = reader.GetString(reader.GetOrdinal("AuthorName")),
                                CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                CoverImage = reader.IsDBNull(reader.GetOrdinal("CoverImage")) ? null : reader.GetString(reader.GetOrdinal("CoverImage")),
                                PublicationDate = reader.GetDateTime(reader.GetOrdinal("PublicationDate"))
                            };
                            results.Add(book);
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Retrieves detailed book information, including author, category, and language details, by the specified book ID.
        /// </summary>
        /// <param name="id">The unique identifier for the book.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the <see cref="Book"/> object
        /// with detailed information including author, category, and language details, if found; otherwise, null.
        /// </returns>
        /// <exception cref="SqlException">Thrown when an error occurs while executing the SQL command.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the connection state is invalid.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
        /// 
        public async Task<Book> GetBookNavigationByIdAsync(int id)
        {
            Book book = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Query to get book details
                // string query = @"EXEC [Books].[SP_GetBookNavgationById] @Id";

                using (SqlCommand command = new SqlCommand(_config.SP_GetBookNavgationById, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue($"@{_config.IdParameterName}", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        // Read book details
                        if (await reader.ReadAsync())
                        {
                            book = new Book
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Description = (reader["Description"] != DBNull.Value) ? reader.GetString(reader.GetOrdinal("Description")) : null,
                                ISBA = reader.GetString(reader.GetOrdinal("ISBA")),
                                PublicationDate = reader.GetDateTime(reader.GetOrdinal("PublicationDate")),
                                CoverImage = (reader["CoverImage"] != DBNull.Value) ? reader.GetString(reader.GetOrdinal("CoverImage")) : null
                            };
                        }

                        // Read author details if available
                        if (await reader.NextResultAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                book.Author = new Author
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Bio = reader.GetString(reader.GetOrdinal("Bio")),
                                    CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    NationalityID = reader.GetInt32(reader.GetOrdinal("NationalityID")),
                                    Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    ProfileImage = reader.IsDBNull(reader.GetOrdinal("ProfileImage")) ? null : reader.GetString(reader.GetOrdinal("ProfileImage"))
                                };
                            }
                        }

                        // Read category details if available
                        if (await reader.NextResultAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                book.Category = new Category
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                    Name = reader.GetString(reader.GetOrdinal("CategoryName")),
                                    CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy"))
                                };
                            }
                        }

                        // Read language details if available
                        if (await reader.NextResultAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                book.Language = new Language
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("LanguageID")),
                                    LanguageName = reader.GetString(reader.GetOrdinal("LanguageName"))
                                };
                            }
                        }
                    }
                }
            }
            return book;
        }

    }
}

