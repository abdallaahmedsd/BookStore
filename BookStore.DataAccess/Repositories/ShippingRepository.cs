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
    public class ShippingRepository : StoredProcedureRepository<Shipping>
    {
        private readonly string _connectionString;
        private readonly ShippingsStoredProcConfiguration _config;

        public ShippingRepository(string connectionString) : base(connectionString, ShippingsStoredProcConfiguration.Instance)
        {
            _connectionString = connectionString;
            _config = ShippingsStoredProcConfiguration.Instance;
        }

        // Not Implemented methods //

        /// <summary>
        /// Deletes a Shipping entity. This method is not supported and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="Id">The ID of the Shipping entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override async Task<bool> Delete(int Id)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Retrieves all Shipping entities. This method is not supported and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override async Task<IEnumerable<Shipping>> GetAllAsync()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Finds a Shipping entity by its ID. This method is not supported and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="Id">The ID of the Shipping entity to find.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override async Task<Shipping?> GetByIdAsync(int Id)
        {
            throw new NotSupportedException();
        }

        ///// <summary>
        ///// Checks if a Shipping entity exists by its ID. This method is not supported and will always throw a <see cref="NotSupportedException"/>.
        ///// </summary>
        ///// <param name="Id">The ID of the Shipping entity to check.</param>
        ///// <returns>A task that represents the asynchronous operation.</returns>
        ///// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        //[Obsolete("This method is not supported in this repository.", error: true)]
        //public override async Task<bool> IsExistsById(int Id)
        //{
        //    throw new NotSupportedException();
        //}

        /// <summary>
        /// Updates a Shipping entity. This method is not supported and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="entity">The Shipping entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override async Task<bool> UpdateAsync(Shipping entity)
        {
            throw new NotSupportedException();
        }

        // Custom methods //

        /// <summary>
        /// Retrieves a Shipping entity by Order ID.
        /// </summary>
        /// <param name="orderId">The Order ID of the Shipping entity to find.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Shipping entity if found, otherwise null.</returns>
        public async Task<Shipping?> GetShippingByOrderIDAsync(int orderId)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                using SqlCommand command = new SqlCommand(_config.GetShippingByOrderID, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderID", orderId);
                connection.Open();
                using SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    return _config.MapEntity(reader);
                }
            }
            catch (SqlException ex)
            {
                throw new RepositoryException($"Database error while retrieving Shipping with Order ID {orderId}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Unexpected error while retrieving Shipping with Order ID {orderId}.", ex);
            }

            return null;
        }


        /// <summary>
        /// Retrieves all shippings for a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result is an enumerable collection of Shipping entities if found; otherwise, null.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the user ID is less than or equal to zero.</exception>
        /// <exception cref="RepositoryException">
        /// Thrown when there is a database error while retrieving shippings
        /// with the specified user ID or an unexpected error occurs.
        /// </exception>
        public async Task<IEnumerable<Shipping>?> GetShippingsByUserId(int userId)
        {
            List<Shipping> shippings = new List<Shipping>();
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                using SqlCommand command = new SqlCommand(_config.GetShippingByOrderID, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", userId);
                connection.Open();
                using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    shippings.Add(_config.MapEntity(reader));
                }
            }
            catch (SqlException ex)
            {
                throw new RepositoryException($"Database error while retrieving Shipping with User ID {userId}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Unexpected error while retrieving Shipping with User ID {userId}.", ex);
            }

            return null;
        }

        /// <summary>
        /// Updates the status of a Shipping entity.
        /// </summary>
        /// <param name="shippingId">The ID of the Shipping entity to update.</param>
        /// <param name="status">The new status.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the operation was successful.</returns>
        public async Task<bool> UpdateShippingStatusAsync(int shippingId, byte status)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                using SqlCommand command = new SqlCommand(_config.UpdateShippingStatus, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"@{_config.IdParameterName}", shippingId);
                command.Parameters.AddWithValue("@Status", status);
                SqlParameter returnValue = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnValue);
                connection.Open();
                await command.ExecuteNonQueryAsync();
                return (int)returnValue.Value == 1;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException($"Database error while updating Shipping status for ID {shippingId}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Unexpected error while updating Shipping status for ID {shippingId}.", ex);
            }
        }
    }
}