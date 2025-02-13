using BookStore.Models.Entities;
using Microsoft.Data.SqlClient;
using RepoSP.Net.Interfaces;
using System;
using System.Data;
using static BookStore.Utilties.DataHelpers.DataHelper;

namespace BookStore.DataAccess.Configurations
{
    /// <summary>
    /// Configuration class for Payment stored procedures.
    /// </summary>
    internal class PaymentsStoredProcConfiguration : IStoredProcConfiguration<Payment>
    {
        /// <summary>
        /// Singleton instance of the PaymentsStoredProcConfiguration class.
        /// </summary>
        private readonly static PaymentsStoredProcConfiguration _instance = new PaymentsStoredProcConfiguration();

        /// <summary>
        /// Gets the singleton instance of the PaymentsStoredProcConfiguration class.
        /// </summary>
        public static PaymentsStoredProcConfiguration Instance => _instance;

        /// <summary>
        /// Gets the name of the ID parameter.
        /// </summary>
        public string IdParameterName => "Id";

        /// <summary>
        /// Gets the name of the output parameter for the new ID.
        /// </summary>
        public string Id_Output_ParameterName => "NewId_output";

        /// <summary>
        /// Gets the name of the stored procedure for getting an entity by ID.
        /// </summary>
        public string GetByIdProcedure => GetStoredProcedureWithSchema("SP_GetPaymentById",Schemas.Sales);

        /// <summary>
        /// Gets the name of the stored procedure for getting all entities.
        /// </summary>
        public string GetAllProcedure => throw new NotImplementedException();

        /// <summary>
        /// Gets the name of the stored procedure for inserting an entity.
        /// </summary>
        public string InsertProcedure => GetStoredProcedureWithSchema("SP_CreatePayment", Schemas.Sales);

        /// <summary>
        /// Gets the name of the stored procedure for updating an entity.
        /// </summary>
        public string UpdateProcedure => GetStoredProcedureWithSchema("SP_UpdatePayment", Schemas.Sales);

        /// <summary>
        /// Gets the name of the stored procedure for deleting an entity.
        /// </summary>
        public string DeleteProcedure => GetStoredProcedureWithSchema("SP_DeletePayment", Schemas.Sales);

        public string DeletePaymentByUserId => GetStoredProcedureWithSchema("SP_DeletePaymentByUserId", Schemas.Sales);


        /// <summary>
        /// Gets the name of the stored procedure for checking if an entity exists by ID.
        /// </summary>
        public string IsExistsByIdProcedure => GetStoredProcedureWithSchema("Fun_IsPaymentExistsById", Schemas.Sales);
        /// <summary>
        /// Gets the name of the stored procedure for retrieving payments by user ID.
        /// </summary>
        public string GetPaymentsByUserID => GetStoredProcedureWithSchema("SP_GetPaymentsByUserID", Schemas.Sales);

        /// <summary>
        /// Gets the name of the stored procedure for retrieving a payment by order ID.
        /// </summary>
        public string GetPaymentByOrderID => GetStoredProcedureWithSchema("SP_GetPaymentByOrderID", Schemas.Sales);

        /// <summary>
        /// Maps the data from the SqlDataReader to a Payment entity.
        /// </summary>
        /// <param name="reader">The SqlDataReader instance.</param>
        /// <returns>A Payment entity.</returns>
        public Payment MapEntity(SqlDataReader reader)
        {
            return new Payment
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
                PaymentDate = reader.GetDateTime(reader.GetOrdinal("PaymentDate")),
                Amount = reader.GetDecimal(reader.GetOrdinal("Amoumt")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
            };
        }

        /// <summary>
        /// Sets the parameters for the insert stored procedure.
        /// </summary>
        /// <param name="command">The SqlCommand instance.</param>
        /// <param name="entity">The Payment entity.</param>
        public void SetInsertParameters(SqlCommand command, Payment entity)
        {
            SqlParameter[] paramsPayment = new SqlParameter[]
            {
                new SqlParameter("@OrderID", SqlDbType.Int) { Value = entity.OrderId },
            };

            command.Parameters.AddRange(paramsPayment);
            SqlParameter sqlParameter = command.Parameters.AddWithValue($"@{Id_Output_ParameterName}", SqlDbType.Int);
            sqlParameter.Direction = ParameterDirection.Output;
        }

        /// <summary>
        /// Sets the parameters for the update stored procedure.
        /// </summary>
        /// <param name="command">The SqlCommand instance.</param>
        /// <param name="entity">The Payment entity.</param>
        public void SetUpdateParameters(SqlCommand command, Payment entity)
        {
            SqlParameter[] paramsPayment =
            {
        new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id },
        new SqlParameter("@OrderID", SqlDbType.Int) { Value = (object)entity.UserId ?? DBNull.Value },
        new SqlParameter("@PaymentDate", SqlDbType.DateTime) { Value = (object)entity.PaymentDate ?? DBNull.Value },
        new SqlParameter("@Amount", SqlDbType.Decimal) { Value = (object)entity.Amount ?? DBNull.Value },
        new SqlParameter("@UserID", SqlDbType.Int) { Value = (object)entity.UserId ?? DBNull.Value }
          };
            command.Parameters.AddRange(paramsPayment);
        }

    }
}
