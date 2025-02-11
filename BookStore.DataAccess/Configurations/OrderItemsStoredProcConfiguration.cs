using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoSP.Net.Interfaces;
using static BookStore.Utilties.DataHelpers.DataHelper;
using BookStore.Models.Entities;

namespace BookstoreBackend.DAL.Configurations
{

    /// <summary>
    /// Internal class implementing IStoredProcConfiguration for OrderItem entities.
    /// Provides singleton instance and stored procedure configuration methods.
    /// </summary>
    internal class OrderItemsStoredProcConfiguration : IStoredProcConfiguration<OrderItem>
    {
        /// <summary>
        /// Singleton instance of the configuration.
        /// </summary>
        private readonly static OrderItemsStoredProcConfiguration _instance = new OrderItemsStoredProcConfiguration();
        public static OrderItemsStoredProcConfiguration Instance => _instance;

        /// <summary>
        /// Stored procedure names for various operations.
        /// </summary>
        public string GetByIdProcedure => GetStoredProcedureWithSchema("GetOrderItemByID", Schemas.Sales);

        public string GetAllProcedure => throw new NotImplementedException();

        public string InsertProcedure => GetStoredProcedureWithSchema("SP_CreateOrderItem", Schemas.Sales);

        public string UpdateProcedure => throw new NotImplementedException();

        public string DeleteProcedure => GetStoredProcedureWithSchema("SP_DeleteOrderItem", Schemas.Sales);

        public string GetAllItemsByOrderId => GetStoredProcedureWithSchema("SP_GetOrderItemsByOrderID", Schemas.Sales);


        public string IsExistsByIdProcedure => GetStoredProcedureWithSchema("Fun_IsOrderItemExistsByID", Schemas.Sales);

        public string IdParameterName => "Id";

        public string Id_Output_ParameterName => "NewId_output";

        /// <summary>
        /// Maps the SQL data reader to OrderItem entity.
        /// </summary>

        public OrderItem MapEntity(SqlDataReader reader)
        {
            return new OrderItem
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                Quntity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
            };
        }

        /// <summary>
        /// Not implemented method to map entity by Id.
        /// </summary>
        public OrderItem MapEntity(int Id, OrderItem entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets parameters for inserting a new OrderItem.
        /// </summary>
        public void SetInsertParameters(SqlCommand command, OrderItem entity)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@OrderID", entity.OrderId),
                new SqlParameter("@BookID", entity.BookId),
                new SqlParameter("@Quantity", entity.Quntity),
            };
            command.Parameters.AddRange(sqlParameters);

            var idParameter = command.Parameters.Add("@NewId_output", System.Data.SqlDbType.Int);
            idParameter.Direction = System.Data.ParameterDirection.Output;
        }

        /// <summary>
        /// Not implemented method to set parameters for updating an OrderItem.
        /// </summary>
        public void SetUpdateParameters(SqlCommand command, OrderItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
