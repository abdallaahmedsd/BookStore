using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using RepoSP.Net.Interfaces;
using BookStore.Models.Entities;

using static BookStore.Utilties.DataHelpers.DataHelper;

namespace BookStore.DataAccess.Configurations
{
    internal class AuthorsStoredProcConfiguration : IStoredProcConfiguration<Author>
    {
        // Singleton pattern
        /// <summary>
        /// Singleton instance of AuthorsStoredProcConfiguration to ensure only one instance is used throughout the application.
        /// </summary>
        private readonly static AuthorsStoredProcConfiguration _instance = new AuthorsStoredProcConfiguration();

        /// <summary>
        /// Provides access to the singleton instance of AuthorsStoredProcConfiguration.
        /// </summary>
        public static AuthorsStoredProcConfiguration Instance => _instance;

        /// <summary>
        /// Gets the stored procedure name for retrieving an author by ID.
        /// </summary>
        public string GetByIdProcedure => GetStoredProcedureWithSchema("SP_GetAuthorByID", Schemas.People);

        /// <summary>
        /// Gets the stored procedure name for retrieving all authors.
        /// </summary>
        public string GetAllProcedure => GetStoredProcedureWithSchema("SP_GetAuthors", Schemas.People);

        /// <summary>
        /// Gets the stored procedure name for inserting a new author.
        /// </summary>
        public string InsertProcedure => GetStoredProcedureWithSchema("SP_CreateAuthor", Schemas.People);

        public string IdParameterName => "Id";

        public string Id_Output_ParameterName => "NewId_output";
        
        /// <summary>
        /// Gets the stored procedure name for updating an existing author.
        /// </summary>
        public string UpdateProcedure => GetStoredProcedureWithSchema("SP_UpdateAuthor", Schemas.People);

        /// <summary>
        /// Gets the stored procedure name for deleting an author.
        /// </summary>
        public string DeleteProcedure => GetStoredProcedureWithSchema("SP_DeleteAuthor", Schemas.People);


        /// <summary>
        /// Stored procedure name for checking if an Author exists by ID.
        /// </summary>
        public string IsExistsByIdProcedure => GetStoredProcedureWithSchema("Fun_IsAuthorExistsById", Schemas.People);
        public string GetAllAuthorsId_Name => GetStoredProcedureWithSchema("GetAllAuthorsId_Name", Schemas.People);

        /// <summary>
        /// Maps the SQL data reader to an Author entity by reading values from the data reader.
        /// </summary>
        /// <param name="reader">The SqlDataReader object containing the author data.</param>
        /// <returns>A mapped Author entity.</returns>
        public Author MapEntity(SqlDataReader reader)
        {
            return new Author
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                NationalityID = reader.GetInt32(reader.GetOrdinal("NationalityID")),
                Phone = (reader["Phone"] == DBNull.Value) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                Bio = (reader["Bio"] == DBNull.Value) ? null : reader.GetString(reader.GetOrdinal("Bio")),
                CreatedBy = Convert.ToInt16(reader["CreatedBy"]),
                ProfileImage = reader["ProfileImage"] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("ProfileImage"))
            };
        }

        /// <summary>
        /// Maps an Author entity to a new instance with a specified ID and copies other properties from the input entity.
        /// </summary>
        /// <param name="Id">The ID to assign to the new Author entity.</param>
        /// <param name="entity">The source Author entity.</param>
        /// <returns>A new Author entity with the specified ID and copied properties.</returns>
        public Author MapEntity(int Id, Author entity)
        {
            return new Author
            {
                Id = Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                NationalityID = entity.NationalityID,
                Phone = entity.Phone,
                Email = entity.Email,
                Bio = entity.Bio,
                CreatedBy = entity.CreatedBy,
                ProfileImage = entity.ProfileImage
            };
        }

        /// <summary>
        /// Configures the SQL command with parameters required for inserting a new author.
        /// </summary>
        /// <param name="command">The SqlCommand object to configure.</param>
        /// <param name="entity">The Author entity containing the data to insert.</param>
        public void SetInsertParameters(SqlCommand command, Author entity)
        {
            SqlParameter[] prarmsAuthor = new SqlParameter[]
            {
                new SqlParameter("@FirstName", entity.FirstName),
                new SqlParameter("@LastName", entity.LastName),
                new SqlParameter("@NationalityID", entity.NationalityID),
                new SqlParameter("@Phone", (entity.Phone != null) ? entity.Phone : DBNull.Value),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@Bio", (entity.Bio != null) ? entity.Bio : DBNull.Value),
                new SqlParameter("@UserId", entity.CreatedBy),
                new SqlParameter("@ProfileImage", (entity.ProfileImage != null) ? entity.ProfileImage : DBNull.Value)
            };
            command.Parameters.AddRange(prarmsAuthor);
            // Parameter for returning the new ID
            var idParameter = command.Parameters.Add("@NewId_output", SqlDbType.Int);
            idParameter.Direction = ParameterDirection.Output;
        }

        /// <summary>
        /// Configures the SQL command with parameters required for updating an existing author.
        /// </summary>
        /// <param name="command">The SqlCommand object to configure.</param>
        /// <param name="entity">The Author entity containing the updated data.</param>
        public void SetUpdateParameters(SqlCommand command, Author entity)
        {
            command.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@FirstName", entity.FirstName),
                new SqlParameter("@LastName", entity.LastName),
                new SqlParameter("@NationalityID", entity.NationalityID),
                new SqlParameter("@Phone", (entity.Phone != null) ? entity.Phone : DBNull.Value),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@Bio", entity.Bio),
                new SqlParameter("@UserId", entity.CreatedBy),
                new SqlParameter("@ProfileImage", (entity.ProfileImage != null) ? entity.ProfileImage : DBNull.Value)
            });
        }

        

    }
}
