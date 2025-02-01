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
    /// Configuration class for Category stored procedures.
    /// Implements the Singleton pattern.
    /// </summary>
    internal class CategoriesStoredProcConfiguration : IStoredProcConfiguration<Category>
    {
        // Singleton instance
        private static readonly CategoriesStoredProcConfiguration _instance = new CategoriesStoredProcConfiguration();

        /// <summary>
        /// Gets the singleton instance of the configuration.
        /// </summary>
        public static CategoriesStoredProcConfiguration Instance => _instance;

        /// <summary>
        /// Gets the name of the parameter used to pass the ID in the stored procedure.
        /// This property returns the parameter name without the '@' prefix.
        /// Example: "IdName" instead of "@IdName".
        /// </summary>
        public string IdParameterName => "Id";

        /// <summary>
        /// Gets the name of the output parameter used to return the ID from the stored procedure.
        /// This property returns the parameter name without the '@' prefix.
        /// Example: "OutputId" instead of "@OutputId".
        /// </summary>
        public string Id_Output_ParameterName => "NewId_output";

        /// <summary>
        /// Gets the stored procedure name for retrieving a Category by ID.
        /// </summary>
        public string GetByIdProcedure => GetStoredProcedureWithSchema("SP_GetCategoryByID", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for retrieving all Categories.
        /// </summary>
        public string GetAllProcedure => GetStoredProcedureWithSchema("SP_GetCategories", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for inserting a new Category.
        /// </summary>
        public string InsertProcedure => GetStoredProcedureWithSchema("SP_CreateCategory", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for updating a Category.
        /// </summary>
        public string UpdateProcedure => GetStoredProcedureWithSchema("SP_UpdateCategory", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for deleting a Category.
        /// </summary>
        public string DeleteProcedure => GetStoredProcedureWithSchema("SP_DeleteCategory", Schemas.Books);

        /// <summary>
        /// Gets the stored procedure name for retrieving a Category by name.
        /// </summary>
        public string GetCategoryByName => GetStoredProcedureWithSchema("SP_GetCategoryByName", Schemas.Books);


        /// <summary>
        /// Gets the stored procedure name that checks if a category exists by ID in the Books schema.
        /// </summary>
        /// <returns>
        /// A string that represents the name of the stored procedure "Fun_IsCategoryExistsByID" in the Books schema.
        /// </returns>
        public string IsExistsByIdProcedure => GetStoredProcedureWithSchema("Fun_IsCategoryExistsByID", Schemas.Books);

        /// <summary>
        /// Maps a SqlDataReader to a Category entity.
        /// </summary>
        /// <param name="reader">The SqlDataReader instance.</param>
        /// <returns>The mapped Category entity.</returns>
        public Category MapEntity(SqlDataReader reader)
        {
            return new Category
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy"))
            };
        }

        /// <summary>
        /// Maps an entity to a Category instance with the specified ID.
        /// </summary>
        /// <param name="Id">The ID of the entity.</param>
        /// <param name="entity">The Category entity.</param>
        /// <returns>The mapped Category entity.</returns>
        public Category MapEntity(int Id, Category entity)
        {
            return new Category
            {
                Id = Id,
                Name = entity.Name,
                CreatedBy = entity.CreatedBy,
            };
        }

        /// <summary>
        /// Sets the parameters for inserting a Category entity.
        /// </summary>
        /// <param name="command">The SqlCommand instance.</param>
        /// <param name="entity">The Category entity.</param>
        public void SetInsertParameters(SqlCommand command, Category entity)
        {
            SqlParameter[] paramsCategory = {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = entity.Name },
                new SqlParameter("@CreatedBy", SqlDbType.Int) { Value = entity.CreatedBy }
            };

            command.Parameters.AddRange(paramsCategory);

            // Parameter for returning the new ID
            SqlParameter idParameter = command.Parameters.Add($"@{Id_Output_ParameterName}", SqlDbType.Int);
            idParameter.Direction = ParameterDirection.Output;
        }

        /// <summary>
        /// Sets the parameters for updating a Category entity.
        /// </summary>
        /// <param name="command">The SqlCommand instance.</param>
        /// <param name="entity">The Category entity.</param>
        public void SetUpdateParameters(SqlCommand command, Category entity)
        {
            SqlParameter[] paramsCategory = {
                new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id },
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = entity.Name },
            };

            command.Parameters.AddRange(paramsCategory);
        }
    }
}
