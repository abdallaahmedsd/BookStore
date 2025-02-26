﻿using Microsoft.Data.SqlClient;
using BookStore.DataAccess.Configurations;
using RepoSP.Net.Services;
using BookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repositories
{
    /// <summary>
    /// Repository for managing ShoppingCard entities using stored procedures.
    /// </summary>
    public class ShoppingCardRepository : StoredProcedureRepository<ShoppingCard>
    {
        private readonly string _connectionString;
        private readonly ShoppingCardsStoredProcConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCardRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        public ShoppingCardRepository(string connectionString)
            : base(connectionString, ShoppingCardsStoredProcConfiguration.Instance)
        {
            _connectionString = connectionString;
            _config = ShoppingCardsStoredProcConfiguration.Instance;
        }

        // Not Implemented methods //
        // Not Implemented methods //

        /// <summary>
        /// Gets all ShoppingCard entities. This method is not supported in this repository and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override async Task<IEnumerable<ShoppingCard>> GetAllAsync()
        {
            throw new NotSupportedException("This method is not supported in this repository.");
        }

        /// <summary>
        /// Deletes a ShoppingCard entity. This method is not supported in this repository and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="id">The ID of the ShoppingCard entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override Task<bool> Delete(int id)
        {
            throw new NotSupportedException("This method is not supported in this repository.");
        }

        /// <summary>
        /// Updates a ShoppingCard entity. This method is not supported in this repository and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="entity">The ShoppingCard entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override Task<bool> UpdateAsync(ShoppingCard entity)
        {
            throw new NotSupportedException("This method is not supported in this repository.");
        }


        // Custom methods //

        /*
         * Async/Await Best Practices:
         *
         * For DeleteShoppingCardItem and DeleteShoppingCardItems, consider using the async keyword and await
         * for asynchronous operations. This approach can improve performance, especially in web applications.
         *
        */

        /// <summary>
        /// Deletes a ShoppingCard item asynchronously.
        /// </summary>
        /// <param name="Id">The ID of the ShoppingCard item to delete.</param>
        /// <param name="CustomerID">The customer ID associated with the ShoppingCard item.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
        public async Task<bool> DeleteAsync(int Id, int CustomerID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.DeleteShoppingCardItem, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue($"@{_config.IdParameterName}", Id);
                        command.Parameters.AddWithValue("@CustomerID", CustomerID);

                        SqlParameter returnValue = new SqlParameter
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnValue);
                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                        return (int)returnValue.Value == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
            }
            return false;
        }

        /// <summary>
        /// Deletes all ShoppingCard items for a customer asynchronously.
        /// </summary>
        /// <param name="CustomerID">The customer ID associated with the ShoppingCard items to delete.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
        public async Task<bool> DeleteCustomerItemsAsync(int CustomerID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.DeleteShoppingCardItems, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CustomerID", CustomerID);

                        SqlParameter returnValue = new SqlParameter
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnValue);
                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                        return (int)returnValue.Value == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
            }
            return false;
        }

        /// <summary>
        /// Gets all ShoppingCard items for a customer asynchronously with pagination.
        /// </summary>
        /// <param name="CustomerID">The customer ID associated with the ShoppingCard items.</param>
        /// <param name="PageNumber">The page number for pagination.</param>
        /// <param name="PageSize">The page size for pagination.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of ShoppingCard items.</returns>
        public async Task<IEnumerable<ShoppingCard>> GetAllAsync(int CustomerID, int PageNumber = 1, int PageSize = 10)
        {
            List<ShoppingCard> listEntities = new List<ShoppingCard>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetShoppingCardItems, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CustomerID", CustomerID);
                        command.Parameters.AddWithValue("@PageNumber", PageNumber);
                        command.Parameters.AddWithValue("@PageSize", PageSize);

                        connection.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                listEntities.Add(_config.MapEntity(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
            return listEntities;
        }
    }
}
