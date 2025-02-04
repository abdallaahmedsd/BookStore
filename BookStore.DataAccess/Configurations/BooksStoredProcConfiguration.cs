using BookStore.Models.Entities;
using RepoSP.Net.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static BookStore.Utilties.DataHelpers.DataHelper;

namespace BookStore.DataAccess.Configurations
{
    /// <summary>
    /// Configuration class for Book stored procedures.
    /// </summary>
    public class BooksStoredProcConfiguration : IStoredProcConfiguration<Book>
    {
        // Singleton instance
        private static readonly BooksStoredProcConfiguration _instance = new BooksStoredProcConfiguration();

        /// <summary>
        /// Gets the singleton instance of the configuration.
        /// </summary>
        public static BooksStoredProcConfiguration Instance => _instance;

        /// <summary>
        /// Gets the name of the ID parameter.
        /// </summary>
        public string IdParameterName => "Id";

        /// <summary>
        /// Gets the name of the output parameter for the new ID.
        /// </summary>
        public string Id_Output_ParameterName => "NewId_output";

        /// <summary>
        /// Gets the stored procedure name for retrieving a Book by ID.
        /// </summary>
        public string GetByIdProcedure => GetStoredProcedureWithSchema("SP_GetBookByID", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for retrieving all Books.
        /// </summary>
        public string GetAllProcedure => GetStoredProcedureWithSchema("SP_GetBooks", Schemas.Books);
        
        /// <summary>
        /// Gets the stored procedure name for inserting a new Book.
        /// </summary>
        public string InsertProcedure => GetStoredProcedureWithSchema("SP_CreateBook", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for updating a Book.
        /// </summary>
        public string UpdateProcedure => GetStoredProcedureWithSchema("SP_UpdateBook", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for deleting a Book.
        /// </summary>
        public string DeleteProcedure => GetStoredProcedureWithSchema("SP_DeleteBook", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for retrieving Books by Author.
        /// </summary>
        public string GetBooksByAuthor => GetStoredProcedureWithSchema("SP_GetBooksByAuthorID", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for retrieving Books by Category ID.
        /// </summary>
        public string GetBooksByCategoryID => GetStoredProcedureWithSchema("SP_GetBooksByCategoryID", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for retrieving a Book by ISBA.
        /// </summary>
        public string GetBookByISBA => GetStoredProcedureWithSchema("SP_GetBookByISBA", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for retrieving a Book by Title.
        /// </summary>
        public string GetBookByTitle => GetStoredProcedureWithSchema("SP_GetBookByTitle", Schemas.Books);


        /// <summary>
        /// Gets the stored procedure name that checks if a book exists by ID in the Books schema.
        /// </summary>
        /// <returns>
        /// A string that represents the name of the stored procedure "Fun_IsBookExistsByID" in the Books schema.
        /// </returns>
        public string IsExistsByIdProcedure => GetStoredProcedureWithSchema("Fun_IsBookExistsByID", Schemas.Books);
        /// <summary>
        /// Gets the stored procedure name for retrieving the top N best-selling books details.
        /// </summary>
        public string GetTopBestSellingBooksDetails => GetStoredProcedureWithSchema("SP_GetTopBestSellingBooksDetails", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for retrieving the top N recently published books.
        /// </summary>
        public string GetTopRecentlyPublishedBooks => GetStoredProcedureWithSchema("SP_GetTopRecentlyPublishedBooks", Schemas.Books);


        /// <summary>
        /// Maps a SqlDataReader to a Book entity.
        /// </summary>
        /// <param name="reader">The SqlDataReader instance.</param>
        /// <returns>The mapped Book entity.</returns>
        public Book MapEntity(SqlDataReader reader)
        {
            return new Book
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                AuthorID = reader.GetInt32(reader.GetOrdinal("AuthorID")),
                CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                LanguageID = reader.GetInt32(reader.GetOrdinal("LanguageID")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                Description = reader["Description"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("Description")) : null,
                ISBA = reader.GetString(reader.GetOrdinal("ISBA")),
                CoverImage = reader["CoverImage"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("CoverImage")) : null,
                PublicationDate = reader.GetDateTime(reader.GetOrdinal("PublicationDate"))
            };
        }

        /// <summary>
        /// Maps an entity to a Book instance with the specified ID.
        /// </summary>
        /// <param name="Id">The ID of the entity.</param>
        /// <param name="entity">The Book entity.</param>
        /// <returns>The mapped Book entity.</returns>
        public Book MapEntity(int Id, Book entity)
        {
            return new Book
            {
                Id = Id,
                AuthorID = entity.AuthorID,
                CategoryID = entity.CategoryID,
                LanguageID = entity.LanguageID,
                Title = entity.Title,
                Price = entity.Price,
                Description = entity.Description,
                ISBA = entity.ISBA,
                CoverImage = entity.CoverImage,
                PublicationDate = entity.PublicationDate
            };
        }

        /// <summary>
        /// Sets the parameters for inserting a Book entity.
        /// </summary>
        /// <param name="command">The SqlCommand instance.</param>
        /// <param name="entity">The Book entity.</param>
        public void SetInsertParameters(SqlCommand command, Book entity)
        {
            SqlParameter[] paramsBook = {
                new SqlParameter("@AuthorID", SqlDbType.Int) { Value = entity.AuthorID },
                new SqlParameter("@CategoryID", SqlDbType.Int) { Value = entity.CategoryID },
                new SqlParameter("@LanguageID", SqlDbType.Int) { Value = entity.LanguageID },
                new SqlParameter("@Title", SqlDbType.NVarChar) { Value = entity.Title },
                new SqlParameter("@Price", SqlDbType.Decimal) { Value = entity.Price },
                new SqlParameter("@Description", SqlDbType.NVarChar) { Value = entity.Description == null ? DBNull.Value : entity.Description },
                new SqlParameter("@ISBA", SqlDbType.NVarChar) { Value = entity.ISBA },
                new SqlParameter("@CoverImage", SqlDbType.NVarChar) { Value = entity.CoverImage == null ? DBNull.Value : entity.CoverImage },
                new SqlParameter("@PublicationDate", SqlDbType.DateTime) { Value = entity.PublicationDate }
            };

            command.Parameters.AddRange(paramsBook);

            // Parameter for returning the new ID
            SqlParameter idParameter = command.Parameters.Add($"@{Id_Output_ParameterName}", SqlDbType.Int);
            idParameter.Direction = ParameterDirection.Output;
        }

        /// <summary>
        /// Sets the parameters for updating a Book entity.
        /// </summary>
        /// <param name="command">The SqlCommand instance.</param>
        /// <param name="entity">The Book entity.</param>
        public void SetUpdateParameters(SqlCommand command, Book entity)
        {
            SqlParameter[] paramsBook = {
                new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id },
                new SqlParameter("@AuthorID", SqlDbType.Int) { Value = entity.AuthorID },
                new SqlParameter("@CategoryID", SqlDbType.Int) { Value = entity.CategoryID },
                new SqlParameter("@LanguageID", SqlDbType.Int) { Value = entity.LanguageID },
                new SqlParameter("@Title", SqlDbType.NVarChar) { Value = entity.Title },
                new SqlParameter("@Price", SqlDbType.Decimal) { Value = entity.Price },
                new SqlParameter("@Description", SqlDbType.NVarChar) { Value = entity.Description == null ? DBNull.Value : entity.Description },
                new SqlParameter("@ISBA", SqlDbType.NVarChar) { Value = entity.ISBA },
                new SqlParameter("@CoverImage", SqlDbType.NVarChar) { Value = entity.CoverImage == null ? DBNull.Value : entity.CoverImage },
                new SqlParameter("@PublicationDate", SqlDbType.DateTime) { Value = entity.PublicationDate },
                new SqlParameter("@UpdatedByUserID", SqlDbType.Int) { Value = entity.UpdatedByUserID }
            };

            command.Parameters.AddRange(paramsBook);
        }
    }
}
