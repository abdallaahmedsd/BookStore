using Microsoft.Data.SqlClient;
using RepoSP.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Entities;
using static BookStore.Utilties.DataHelpers.DataHelper;

namespace BookstoreBackend.DAL.Configurations
{
    /// <summary>
    /// Internal class implementing IStoredProcConfiguration for Order entities.
    /// Provides singleton instance and stored procedure configuration methods.
    /// </summary>
    internal class OrdersStoredProcConfiguration : IStoredProcConfiguration<Order>
    {
        /// <summary>
        /// Singleton instance of the configuration.
        /// </summary>
        private readonly static OrdersStoredProcConfiguration _instance = new OrdersStoredProcConfiguration();
        public static OrdersStoredProcConfiguration Instance => _instance;

        /// <summary>
        /// Stored procedure names for various operations.
        /// </summary>

        public string IdParameterName => "Id";
        public string Id_Output_ParameterName => "NewId_output";
        public string GetByIdProcedure => GetStoredProcedureWithSchema("SP_GetOrderByID", Schemas.Sales);
        public string GetAllProcedure => GetStoredProcedureWithSchema("SP_GetOrders", Schemas.Sales);
        public string InsertProcedure => GetStoredProcedureWithSchema("SP_CreateOrder", Schemas.Sales);
        public string UpdateProcedure => GetStoredProcedureWithSchema("SP_UpdateOrder", Schemas.Sales);
        public string DeleteProcedure => GetStoredProcedureWithSchema("SP_DeleteOrder", Schemas.Sales);
        public string GetOrdersByUserID => GetStoredProcedureWithSchema("SP_GetOrdersByUserID", Schemas.Sales);
        public string IsExistsByIdProcedure => GetStoredProcedureWithSchema("Fun_IsOrderExistsById", Schemas.Sales);
        public string UpdateOrderStatus => GetStoredProcedureWithSchema("SP_UpdateOrderStatus", Schemas.Sales);



        /// <summary>
        /// Maps the SQL data reader to Order entity.
        /// </summary>
        /// <param name="reader">SQL data reader.</param>
        /// <returns>Mapped Order entity.</returns>
        public Order MapEntity(SqlDataReader reader)
        {
            return new Order
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Status = reader.GetByte(reader.GetOrdinal("Status")),
                TotalAmoumt = reader.GetDecimal(reader.GetOrdinal("TotalAmoumt")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                UserID = reader.GetInt32(reader.GetOrdinal("UserId")),
            };
        }

        /// <summary>
        /// Maps an entity by Id.
        /// </summary>
        /// <param name="Id">Order Id.</param>
        /// <param name="entity">Order entity.</param>
        /// <returns>Mapped Order entity with the specified Id.</returns>

        public Order MapEntity(int Id, Order entity)
        {
            return new Order
            {
                Id = Id,
                Status = entity.Status,
                TotalAmoumt = entity.TotalAmoumt,
                CreatedDate = entity.CreatedDate,
                UserID = entity.UserID,
            };
        }

        /// <summary>
        /// Sets parameters for inserting a new Order.
        /// </summary>
        /// <param name="command">SQL command.</param>
        /// <param name="entity">Order entity.</param>
        public void SetInsertParameters(SqlCommand command, Order entity)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", entity.UserID),
                new SqlParameter("@TotalAmoumt", entity.TotalAmoumt),
                new SqlParameter("@Status", entity.Status),
            };
            command.Parameters.AddRange(parameters);

            var idParameter = command.Parameters.Add("@NewId_output", System.Data.SqlDbType.Int);
            idParameter.Direction = System.Data.ParameterDirection.Output;
        }

        /// <summary>
        /// Sets parameters for updating an Order.
        /// </summary>
        /// <param name="command">SQL command.</param>
        /// <param name="entity">Order entity.</param>
        public void SetUpdateParameters(SqlCommand command, Order entity)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@TotalAmoumt", entity.TotalAmoumt),
                new SqlParameter("@Status", entity.Status),
            };
            command.Parameters.AddRange(parameters);
        }
    }
}
