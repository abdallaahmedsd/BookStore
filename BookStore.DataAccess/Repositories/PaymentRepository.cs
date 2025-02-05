using BookStore.DataAccess.Configurations;
using BookStore.Models.Entities;
using Microsoft.Data.SqlClient;
using RepoSP.Net.Exceptions;
using RepoSP.Net.Interfaces;
using RepoSP.Net.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repositories
{
    /// <summary>
    /// Repository for managing Payment entities using stored procedures.
    /// </summary>
    public class PaymentRepository : StoredProcedureRepository<Payment>
    {
        private readonly string _connectionString;
        private readonly PaymentsStoredProcConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        public PaymentRepository(string connectionString) : base(connectionString, PaymentsStoredProcConfiguration.Instance)
        {
            _connectionString = connectionString;
            _config = PaymentsStoredProcConfiguration.Instance;
        }

        // Not Implemented methods //

        /// <summary>
        /// Deletes a Payment entity. This method is not supported and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="Id">The ID of the Payment entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override async Task<bool> Delete(int Id)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Retrieves all Payment entities. This method is not supported and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override async Task<IEnumerable<Payment>> GetAllAsync()
        {
            throw new NotSupportedException();
        }


        /// <summary>
        /// Updates a Payment entity. This method is not supported and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="entity">The Payment entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override async Task<bool> UpdateAsync(Payment entity)
        {
            throw new NotSupportedException();
        }

        // Custom methods //

        /// <summary>
        /// Retrieves all payments for a specific user with pagination support.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="pageNumber">The page number for pagination (default is 1).</param>
        /// <param name="pageSize">The number of items per page (default is 10).</param>
        /// <returns>A collection of Payment entities if found; otherwise, an empty collection.</returns>
        public async Task<IEnumerable<Payment>> GetPaymentsByUserIDAsync(int userId, int pageNumber = 1, int pageSize = 10)
        {
            if (userId <= 0 || pageNumber <= 0 || pageSize <= 0)
            {
                return Enumerable.Empty<Payment>();
            }

            List<Payment> payments = new List<Payment>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(_config.GetPaymentsByUserID, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@PageNumber", pageNumber);
                    command.Parameters.AddWithValue("@PageSize", pageSize);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            payments.Add(_config.MapEntity(reader));
                        }
                    }
                }
            }
            return payments;
        }

        /// <summary>
        /// Retrieves the payment details for a specific order.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>A Payment entity if found; otherwise, null.</returns>
        public async Task<Payment?> GetPaymentByOrderIDAsync(int orderId)
        {
            if (orderId <= 0)
            {
                return null;
            }

            Payment? payment = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(_config.GetPaymentByOrderID, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderId", orderId);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            payment = _config.MapEntity(reader);
                        }
                    }
                }
            }
            return payment;
        }

    }
}
