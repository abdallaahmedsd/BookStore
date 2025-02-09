using Microsoft.Data.SqlClient;
using BookStore.Models.Entities;
using static BookStore.Utilties.DataHelpers.DataHelper;
using RepoSP.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Configurations
{
    /// <summary>
    /// Configuration class for ShoppingCard stored procedures.
    /// Implements the Singleton pattern.
    /// </summary>
    internal class ShoppingCardsStoredProcConfiguration : IStoredProcConfiguration<ShoppingCard>
    {
        // Singleton instance
        private static readonly ShoppingCardsStoredProcConfiguration _instance = new ShoppingCardsStoredProcConfiguration();

        /// <summary>
        /// Gets the singleton instance of the configuration.
        /// </summary>
        public static ShoppingCardsStoredProcConfiguration Instance => _instance;

        /// <summary>
        /// Gets the stored procedure name for retrieving a ShoppingCard by ID.
        /// </summary>
        public string GetByIdProcedure => GetStoredProcedureWithSchema("SP_GetShoppingCardItem", Schemas.Sales);

        /// <summary>
        /// Gets the stored procedure name for retrieving all ShoppingCards.
        /// </summary>
        public string GetAllProcedure => throw new NotImplementedException();

        /// <summary>
        /// Gets the stored procedure name for inserting a new ShoppingCard.
        /// </summary>
        public string InsertProcedure => GetStoredProcedureWithSchema("SP_CreateShoppingCardItem", Schemas.Sales);

        /// <summary>
        /// Gets the stored procedure name for updating a ShoppingCard.
        /// </summary>
        public string UpdateProcedure => GetStoredProcedureWithSchema("SP_UpdateShoppingCard", Schemas.Sales);

        /// <summary>
        /// Gets the stored procedure name for deleting a ShoppingCard.
        /// </summary>
        public string DeleteProcedure => throw new NotImplementedException();

        /// <summary>
        /// Gets the stored procedure name for deleting a single ShoppingCard item.
        /// </summary>
        public string DeleteShoppingCardItem => GetStoredProcedureWithSchema("SP_DeleteShoppingCardItem", Schemas.Sales);

        /// <summary>
        /// Gets the stored procedure name for deleting all ShoppingCard items.
        /// </summary>
        public string DeleteShoppingCardItems => GetStoredProcedureWithSchema("SP_DeleteShoppingCardItems", Schemas.Sales);

        /// <summary>
        /// Gets the stored procedure name for retrieving ShoppingCard items.
        /// </summary>
        public string GetShoppingCardItems => GetStoredProcedureWithSchema("SP_GetShoppingCardItems", Schemas.Sales);

        public string GetShoppingCardByUserID => GetStoredProcedureWithSchema("SP_GetShoppingCardByUserID", Schemas.Sales);
        public string GetShoppingCardByUserIDandBookId => GetStoredProcedureWithSchema("SP_GetShoppingCardByUserIDandBookId", Schemas.Sales);

        

        /// <summary>
        /// Stored procedure name for checking if a ShippingCard exists by ID.
        /// </summary>
        public string IsExistsByIdProcedure => GetStoredProcedureWithSchema("Fun_IsShippingCardExistsById", Schemas.Sales);

        /// <summary>
        /// Gets the name of the ID parameter.
        /// </summary>
        public string IdParameterName => "Id";

        /// <summary>
        /// Gets the name of the output parameter for the new ID.
        /// </summary>
        public string Id_Output_ParameterName => "NewId_output";

        /// <summary>
        /// Maps a SqlDataReader to a ShoppingCard entity.
        /// </summary>
        /// <param name="reader">The SqlDataReader instance.</param>
        /// <returns>The mapped ShoppingCard entity.</returns>
        public ShoppingCard MapEntity(SqlDataReader reader)
        {
            return new ShoppingCard
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                BookID = reader.GetInt32(reader.GetOrdinal("BookID")),
                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
                UserID = reader.GetInt32(reader.GetOrdinal("UserID"))
            };
        }

        /// <summary>
        /// Maps an entity to a ShoppingCard instance with the specified ID.
        /// </summary>
        /// <param name="Id">The ID of the entity.</param>
        /// <param name="entity">The ShoppingCard entity.</param>
        /// <returns>The mapped ShoppingCard entity.</returns>
        //public ShoppingCard MapEntity(int Id, ShoppingCard entity)
        //{
        //    return new ShoppingCard
        //    {
        //        Id = Id,
        //        BookID = entity.BookID,
        //        Quantity = entity.Quantity,
        //        SubTotal = entity.SubTotal,
        //        UserID = entity.UserID
        //    };
        //}

        /// <summary>
        /// Sets the parameters for inserting a ShoppingCard entity.
        /// </summary>
        /// <param name="command">The SqlCommand instance.</param>
        /// <param name="entity">The ShoppingCard entity.</param>
        public void SetInsertParameters(SqlCommand command, ShoppingCard entity)
        {
            SqlParameter[] paramsShoppingCard = {
                new SqlParameter("@BookID", SqlDbType.Int) { Value = entity.BookID },
                new SqlParameter("@Quantity", SqlDbType.Int) { Value = entity.Quantity },
                new SqlParameter("@CustomerID", SqlDbType.Int) { Value = entity.UserID }
            };
            command.Parameters.AddRange(paramsShoppingCard);
            SqlParameter idParameter = command.Parameters.Add($"@{Id_Output_ParameterName}", SqlDbType.Int);
            idParameter.Direction = ParameterDirection.Output;
        }

        /// <summary>
        /// Sets the parameters for updating a ShoppingCard entity.
        /// </summary>
        /// <param name="command">The SqlCommand instance.</param>
        /// <param name="entity">The ShoppingCard entity.</param>
        public void SetUpdateParameters(SqlCommand command, ShoppingCard entity)
        {
            SqlParameter[] paramsShoppingCard = {
                new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id },
                new SqlParameter("@BookID", SqlDbType.Int) { Value = entity.BookID },
                new SqlParameter("@Quantity", SqlDbType.Int) { Value = entity.Quantity },
                new SqlParameter("@CustomerID", SqlDbType.Int) { Value = entity.UserID }
            };
            command.Parameters.AddRange(paramsShoppingCard);
        }
    }
}
