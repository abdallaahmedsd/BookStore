using Microsoft.Data.SqlClient;
using BookStore.Models.Entities;
using static BookStore.Utilties.DataHelpers.DataHelper;
using RepoSP.Net.Interfaces;
using System;
using System.Data;

namespace BookStore.DataAccess.Configurations
{
    /// <summary>
    /// Configuration class for Shipping stored procedures.
    /// Implements the Singleton pattern.
    /// </summary>
    internal class ShippingsStoredProcConfiguration : IStoredProcConfiguration<Shipping>
    {
        // Singleton instance
        private static readonly ShippingsStoredProcConfiguration _instance = new ShippingsStoredProcConfiguration();

        /// <summary>
        /// Gets the singleton instance of the configuration.
        /// </summary>
        public static ShippingsStoredProcConfiguration Instance => _instance;

        // Get By IDs stored procedures //

        /// <summary>
        /// Gets the stored procedure name for retrieving a Shipping by ID.
        /// </summary>
        public string GetByIdProcedure => GetStoredProcedureWithSchema("SP_GetShippingByID", Schemas.Sales);

        /// <summary>
        /// Gets the stored procedure name for retrieving a Shipping by Order ID.
        /// </summary>
        public string GetShippingByOrderID => GetStoredProcedureWithSchema("SP_GetShippingByOrderID", Schemas.Sales);

        /// <summary>
        /// Gets the stored procedure name for retrieving Orders by User ID.
        /// </summary>
        public string GetShippingsByUserId => GetStoredProcedureWithSchema("SP_GetShippingsByUserID", Schemas.Sales);

        // Get All stored procedures //

        /// <summary>
        /// Gets the stored procedure name for retrieving all Shippings.
        /// </summary>
        public string GetAllProcedure => throw new NotImplementedException();


        // Insert stored procedures //

        /// <summary>
        /// Gets the stored procedure name for inserting a new Shipping.
        /// </summary>
        public string InsertProcedure => GetStoredProcedureWithSchema("SP_CreateShipping", Schemas.Sales);


        // Update stored procedures //

        /// <summary>
        /// Gets the stored procedure name for updating a Shipping.
        /// </summary>
        public string UpdateProcedure => GetStoredProcedureWithSchema("SP_UpdateShipping", Schemas.Sales);

        ///// <summary>
        ///// Gets the stored procedure name for updating Shipping status.
        ///// </summary>
        //public string UpdateShippingStatus => GetStoredProcedureWithSchema("SP_UpdateShippingStatus", Schemas.Sales);

        // Delete stored procedures //

        /// <summary>
        /// Gets the stored procedure name for deleting a Shipping.
        /// </summary>
        public string DeleteProcedure => throw new NotImplementedException();

        // Check if exists stored procedures //

        /// <summary>
        /// Gets the stored procedure name for checking if a Shipping exists by ID.
        /// </summary>
        public string IsExistsByIdProcedure => GetStoredProcedureWithSchema("Fun_IsShippingExistsById", Schemas.Sales);


        // Parameter Names //

        /// <summary>
        /// Gets the name of the ID parameter.
        /// </summary>
        public string IdParameterName => "Id";

        /// <summary>
        /// Gets the name of the output parameter for the new ID.
        /// </summary>
        public string Id_Output_ParameterName => "NewId_output";


        /// <summary>
        /// Maps a SqlDataReader to a Shipping entity.
        /// </summary>
        /// <param name="reader">The SqlDataReader instance.</param>
        /// <returns>The mapped Shipping entity.</returns>
        public Shipping MapEntity(SqlDataReader reader)
        {
            return new Shipping
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                ShippingDate = reader["ShippingDate"] != DBNull.Value ? (DateTime?)reader.GetDateTime(reader.GetOrdinal("ShippingDate")) : null,
                TrackingNumber = reader["TrackingNumber"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("TrackingNumber")) : null,
                EstimatedDelivery = reader["EstimatedDelivery"] != DBNull.Value ? (DateTime?)reader.GetDateTime(reader.GetOrdinal("EstimatedDelivery")) : null,
                CountryID = reader.GetInt32(reader.GetOrdinal("CountryID")),
                City = reader.GetString(reader.GetOrdinal("City")),
                Address = reader.GetString(reader.GetOrdinal("Address")),
                ZipCode = reader.GetString(reader.GetOrdinal("ZipCode")),
                Carrier = reader["Carrier"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("Carrier")) : null
            };
        }

        /// <summary>
        /// Sets the parameters for inserting a Shipping entity.
        /// </summary>
        /// <param name="command">The SqlCommand instance.</param>
        /// <param name="entity">The Shipping entity.</param>
        public void SetInsertParameters(SqlCommand command, Shipping entity)
        {
            SqlParameter[] paramsShipping =
            {
                new SqlParameter("@OrderID", SqlDbType.Int) { Value = entity.OrderID },
                new SqlParameter("@CountryID", SqlDbType.Int) { Value = entity.CountryID },
                new SqlParameter("@City", SqlDbType.NVarChar, 100) { Value = entity.City },
                new SqlParameter("@Address", SqlDbType.NVarChar, 255) { Value = entity.Address },
                new SqlParameter("@ZipCode", SqlDbType.NVarChar, 20) { Value = entity.ZipCode },
                new SqlParameter("@ShippingDate", SqlDbType.DateTime) { Value = entity.ShippingDate ?? (object)DBNull.Value },
                new SqlParameter("@TrackingNumber", SqlDbType.NVarChar, 100) { Value = entity.TrackingNumber ?? (object)DBNull.Value },
                new SqlParameter("@EstimatedDelivery", SqlDbType.DateTime) { Value = entity.EstimatedDelivery ?? (object)DBNull.Value },
                new SqlParameter("@Carrier", SqlDbType.NVarChar, 100) { Value = entity.Carrier ?? (object)DBNull.Value }
            };
            command.Parameters.AddRange(paramsShipping);
            SqlParameter idParameter = command.Parameters.Add($"@{Id_Output_ParameterName}", SqlDbType.Int);
            idParameter.Direction = ParameterDirection.Output;
        }

        /// <summary>
        /// Sets the parameters for updating a Shipping entity.
        /// </summary>
        /// <param name="command">The SqlCommand instance.</param>
        /// <param name="entity">The Shipping entity.</param>
        public void SetUpdateParameters(SqlCommand command, Shipping entity)
        {
            SqlParameter[] paramsShipping =
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id },
                new SqlParameter("@OrderID", SqlDbType.Int) { Value = entity.OrderID },
                new SqlParameter("@CountryID", SqlDbType.Int) { Value = entity.CountryID },
                new SqlParameter("@City", SqlDbType.NVarChar, 100) { Value = entity.City },
                new SqlParameter("@Address", SqlDbType.NVarChar, 255) { Value = entity.Address },
                new SqlParameter("@ZipCode", SqlDbType.NVarChar, 20) { Value = entity.ZipCode },
                new SqlParameter("@ShippingDate", SqlDbType.DateTime) { Value = entity.ShippingDate ?? (object)DBNull.Value },
                new SqlParameter("@TrackingNumber", SqlDbType.NVarChar, 100) { Value = entity.TrackingNumber ?? (object)DBNull.Value },
                new SqlParameter("@EstimatedDelivery", SqlDbType.DateTime) { Value = entity.EstimatedDelivery ?? (object)DBNull.Value },
                new SqlParameter("@Carrier", SqlDbType.NVarChar, 100) { Value = entity.Carrier ?? (object)DBNull.Value }
            };
            command.Parameters.AddRange(paramsShipping);
        }

    }
}
