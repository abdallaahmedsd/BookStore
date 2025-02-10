using Microsoft.Data.SqlClient;
using BookStore.DataAccess.Configurations;
using RepoSP.Net.Services;
using BookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.ViewModels.Customer.Cart;

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
        public async Task<IEnumerable<ShoppingCard>> GetShoppingCartByUserIdWithPaginationAsync(int CustomerID, int PageNumber = 1, int PageSize = 10)
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
        /// <summary>
        /// Retrieves the shopping card items for a specific user by their user ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable list of shopping cards, or null if an error occurs.</returns>
        public async Task<IEnumerable<ShoppingCard>?> GetShoppingCardByUserIDAsync(int userId)
        {
            HashSet<ShoppingCard> listShoppingCards = new HashSet<ShoppingCard>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetShoppingCardByUserID, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", userId);
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                listShoppingCards.Add(_config.MapEntity(reader));
                            }

                            return listShoppingCards;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exception
            }
            catch (Exception ex)
            {
                // Handle general exception
            }
            return null;
        }

        /// <summary>
        /// Retrieves a shopping card item for a specific user and book ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="bookId">The book ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a shopping card, or null if an error occurs.</returns>
        public async Task<ShoppingCard?> GetShoppingCardByUserIDandBookIdAsync(int userId, int bookId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetShoppingCardByUserIDandBookId, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", userId);
                        command.Parameters.AddWithValue("@BookID", bookId);
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                return _config.MapEntity(reader);
                            }


                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exception
            }
            catch (Exception ex)
            {
                // Handle general exception
            }
            return null;
        }

        /// <summary>
        /// Retrieves the count of shopping items for a specific user by their user ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the count of shopping items, or null if an error occurs.</returns>
        public async Task<int?> GetShoppingItemsCountByUserIdAsync(int userId)
        {
            int? shoppingItemsCount = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetShoppingItemsCountByUserId, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", userId);
                        await connection.OpenAsync();
                        var result = await command.ExecuteScalarAsync();
                        if (result != null && int.TryParse(result.ToString(), out int itemsCount))
                        {
                            shoppingItemsCount = itemsCount;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exception
            }
            catch (Exception ex)
            {
                // Handle general exception
            }
            return shoppingItemsCount;
        }


        public async Task<List<CartViewModel>> GetShoppingCartViewModelAsync(int userId)
        {
            List<CartViewModel> shoppingCart = new List<CartViewModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("Sales.SP_ShoppingCartViewModel", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userId);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            CartViewModel item = new CartViewModel
                            {
                                Id = (int)reader["Id"],
                                BookID = (int)reader["BookID"],
                                BookCoverImage = reader["CoverImage"].ToString(),
                                BookTitle = reader["Title"].ToString(),
                                BookPrice = (decimal)reader["Price"],
                                SubTotal = (decimal)reader["SubTotal"],
                                Quantity = (int)reader["Quantity"],
                                UserID = (int)reader["UserID"]
                                // Ensure all properties are correctly mapped
                            };

                            shoppingCart.Add(item);
                        }
                    }
                }
            }

            return shoppingCart;


        }
    }
}
