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
    /// Repository for managing Shipping entities.
    /// </summary>
    public class ShippingRepository : StoredProcedureRepository<Shipping>
    {
        private readonly string _connectionString;
        private readonly ShippingsStoredProcConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShippingRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public ShippingRepository(string connectionString) : base(connectionString, ShippingsStoredProcConfiguration.Instance)
        {
            _connectionString = connectionString;
            _config = ShippingsStoredProcConfiguration.Instance;
        }

        /// <summary>
        /// Deletes a Shipping entity. This method is not supported.
        /// </summary>
        /// <param name="Id">The ID of the Shipping entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override Task<bool> Delete(int Id)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Retrieves all Shipping entities. This method is not supported.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override Task<IEnumerable<Shipping>> GetAllAsync()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Updates a Shipping entity. This method is not supported.
        /// </summary>
        /// <param name="entity">The Shipping entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override Task<bool> UpdateAsync(Shipping entity)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Retrieves a Shipping entity by Order ID.
        /// </summary>
        /// <param name="orderId">The Order ID of the Shipping entity to find.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Shipping entity if found, otherwise null.</returns>
        /// <exception cref="RepositoryException">
        /// Thrown when there is a database error while retrieving the Shipping entity.
        /// </exception>
        public async Task<Shipping?> GetShippingByOrderIDAsync(int orderId)
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                await using SqlConnection connection = new SqlConnection(_connectionString);
                await using SqlCommand command = new SqlCommand(_config.GetShippingByOrderID, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@OrderID", orderId);
                await connection.OpenAsync();
                await using SqlDataReader reader = await command.ExecuteReaderAsync();
                return reader.Read() ? _config.MapEntity(reader) : null;
            });
        }

        /// <summary>
        /// Retrieves all Shipping entities for a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result is an enumerable collection of Shipping entities if found; otherwise, null.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the user ID is less than or equal to zero.</exception>
        /// <exception cref="RepositoryException">
        /// Thrown when there is a database error while retrieving the Shipping entities.
        /// </exception>
        public async Task<IEnumerable<Shipping>?> GetShippingsByUserId(int userId)
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                List<Shipping> shippings = new List<Shipping>();
                await using SqlConnection connection = new SqlConnection(_connectionString);
                await using SqlCommand command = new SqlCommand(_config.GetShippingsByUserId, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@UserID", userId);
                await connection.OpenAsync();
                await using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    shippings.Add(_config.MapEntity(reader));
                }
                return shippings;
            });
        }

        /// <summary>
        /// Updates the status of a Shipping entity.
        /// </summary>
        /// <param name="shippingId">The ID of the Shipping entity to update.</param>
        /// <param name="status">The new status.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the operation was successful.</returns>
        /// <exception cref="RepositoryException">
        /// Thrown when there is a database error while updating the Shipping entity status.
        /// </exception>
        public async Task<bool> UpdateShippingStatusAsync(int shippingId, byte status)
        {
            return await ExecuteWithExceptionHandling(async () =>
            {
                await using SqlConnection connection = new SqlConnection(_connectionString);
                await using SqlCommand command = new SqlCommand(_config.UpdateShippingStatus, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue($"@{_config.IdParameterName}", shippingId);
                command.Parameters.AddWithValue("@Status", status);
                SqlParameter returnValue = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnValue);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                return (int)returnValue.Value == 1;
            });
        }

        /// <summary>
        /// Executes an asynchronous operation with exception handling.
        /// </summary>
        /// <typeparam name="T">The type of the task result.</typeparam>
        /// <param name="operation">The asynchronous operation to execute.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the result of the operation if successful; otherwise, null.</returns>
        /// <exception cref="RepositoryException">
        /// Thrown when there is a database error or an unexpected error during the operation.
        /// </exception>
        private async Task<T?> ExecuteWithExceptionHandling<T>(Func<Task<T?>> operation)
        {
            try
            {
                return await operation();
            }
            catch (SqlException ex)
            {
                throw new RepositoryException($"Database error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Unexpected error: {ex.Message}", ex);
            }
        }
    }
}
