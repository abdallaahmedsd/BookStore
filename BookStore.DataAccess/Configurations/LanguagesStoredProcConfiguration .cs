using BookStore.Models.Entities;
using Microsoft.Data.SqlClient;
using RepoSP.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookStore.Utilties.DataHelpers.DataHelper;


namespace BookStore.DataAccess.Configurations
{
    /// <summary>
    /// Configuration class for Language stored procedures.
    /// Implements the Singleton pattern.
    /// </summary>
    internal class LanguagesStoredProcConfiguration : IStoredProcConfiguration<Language>
    {
        // Singleton instance
        private static readonly LanguagesStoredProcConfiguration _instance = new LanguagesStoredProcConfiguration();

        /// <summary>
        /// Gets the singleton instance of the configuration.
        /// </summary>
        public static LanguagesStoredProcConfiguration Instance => _instance;

        /// <summary>
        /// Gets the stored procedure name for retrieving a Language by ID.
        /// </summary>
        public string GetByIdProcedure => GetStoredProcedureWithSchema("SP_GetLanguageByID", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for retrieving all Languages.
        /// </summary>
        public string GetAllProcedure => GetStoredProcedureWithSchema("SP_GetLanguages", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for inserting a new Language.
        /// </summary>
        public string InsertProcedure => throw new NotImplementedException();

        /// <summary>
        /// Gets the stored procedure name for updating a Language.
        /// </summary>
        public string UpdateProcedure => throw new NotImplementedException();

        /// <summary>
        /// Gets the stored procedure name for deleting a Language.
        /// </summary>
        public string DeleteProcedure => throw new NotImplementedException();
        public string GetByNameProcedure => GetStoredProcedureWithSchema("SP_GetLanguageByName", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name that checks if a language exists by ID in the Books schema.
        /// </summary>
        /// <returns>
        /// A string that represents the name of the stored procedure "Fun_IsLanguageExistsByID" in the Books schema.
        /// </returns>
        public string IsExistsByIdProcedure => GetStoredProcedureWithSchema("Fun_IsLanguageExistsByID", Schemas.Books);

        /// <summary>
        /// Gets the name of the ID parameter.
        /// </summary>
        public string IdParameterName => "Id";

        /// <summary>
        /// Gets the name of the output parameter for the new ID.
        /// </summary>
        public string Id_Output_ParameterName => "NewId_output";


        /// <summary>
        /// Maps a SqlDataReader to a Language entity.
        /// </summary>
        /// <param name="reader">The SqlDataReader instance.</param>
        /// <returns>The mapped Language entity.</returns>
        public Language MapEntity(SqlDataReader reader)
        {
            return new Language
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                LanguageName = reader.GetString(reader.GetOrdinal("LanguageName"))
            };
        }

        /// <summary>
        /// Maps an entity to a Language instance with the specified ID.
        /// </summary>
        /// <param name="Id">The ID of the entity.</param>
        /// <param name="entity">The Language entity.</param>
        /// <returns>The mapped Language entity.</returns>
        //public Language MapEntity(int Id, Language entity)
        //{
        //    return new Language
        //    {
        //        Id = Id,
        //        LanguageName = entity.LanguageName
        //    };
        //}

        /// <summary>
        /// Sets the parameters for inserting a Language entity.
        /// </summary>
        /// <param name="command">The SqlCommand instance.</param>
        /// <param name="entity">The Language entity.</param>
        public void SetInsertParameters(SqlCommand command, Language entity)
        {
            SqlParameter[] paramsLanguage = {
                new SqlParameter("@LanguageName", SqlDbType.NVarChar) { Value = entity.LanguageName }
            };
            command.Parameters.AddRange(paramsLanguage);
            SqlParameter idParameter = command.Parameters.Add($"@{Id_Output_ParameterName}", SqlDbType.Int);
            idParameter.Direction = ParameterDirection.Output;
        }

        /// <summary>
        /// Sets the parameters for updating a Language entity.
        /// </summary>
        /// <param name="command">The SqlCommand instance.</param>
        /// <param name="entity">The Language entity.</param>
        public void SetUpdateParameters(SqlCommand command, Language entity)
        {
            SqlParameter[] paramsLanguage = {
                new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id },
                new SqlParameter("@LanguageName", SqlDbType.NVarChar) { Value = entity.LanguageName }
            };
            command.Parameters.AddRange(paramsLanguage);
        }
    }
}
