using BookstoreBackend.DAL.Configurations;
using Microsoft.Data.SqlClient;
using RepoSP.Net.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Entities;
using System.Data;
using BookStore.Models.ViewModels.Customer.OrderVM;
using BookStore.Models.ViewModels.Admin.Order;

namespace BookStore.DataAccess.Repositories
{
    /// <summary>
    /// Repository for managing Order entities using stored procedures.
    /// </summary>
    public class OrderRepositry : StoredProcedureRepository<Order>
    {
        private readonly string _connectionString;
        private readonly OrdersStoredProcConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepositry"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        public OrderRepositry(string connectionString)
            : base(connectionString, OrdersStoredProcConfiguration.Instance)
        {
            _connectionString = connectionString;
            _config = OrdersStoredProcConfiguration.Instance;
        }

        /// <summary>
        /// Gets the orders by user ID asynchronously with pagination.
        /// </summary>
        /// <param name="userId">The user ID associated with the orders.</param>
        /// <param name="PageNumber">The page number for pagination.</param>
        /// <param name="PageSize">The page size for pagination.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of orders.</returns>
        public async Task<IEnumerable<Order>> GetOrdersByUserIDAsync(int userId, int PageNumber = 1, int PageSize = 10)
        {
            
            var listOfOrders = new List<Order>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetOrdersByUserID, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", userId);
                        command.Parameters.AddWithValue("@PageNumber", PageNumber);
                        command.Parameters.AddWithValue("@PageSize", PageSize);
                        connection.Open();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                listOfOrders.Add(_config.MapEntity(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
            }
            return listOfOrders;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, byte status)
        {
            
                await using SqlConnection connection = new SqlConnection(_connectionString);
                await using SqlCommand command = new SqlCommand(_config.UpdateOrderStatus, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue($"@{_config.IdParameterName}", orderId);
                command.Parameters.AddWithValue("@Status", status);
                SqlParameter returnValue = new SqlParameter
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(returnValue);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                return (int)returnValue.Value == 1;
        }

        public async Task<IEnumerable<OrderListViewModel>> GetOrderListViewModelAsync()
        {
            List<OrderListViewModel> collection = new List<OrderListViewModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Sales.SP_GetOrderListViewModel", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        await conn.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var orderListViewmodel = new OrderListViewModel()
                                {
                                    Id = reader.GetInt32(0),
                                    CreatedDate = reader.GetDateTime(1),
                                    TotalAmoumt = reader.GetDecimal(2),
                                    Status = reader.GetByte(3)
                                };

                                collection.Add(orderListViewmodel);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Database error: " + ex.Message);
                    }
                }
            }

            return collection;
        }

        public async Task<OrderDetailsViewModel?> GetOrderDetailsViewModleByOrderId(int orderId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);

            using SqlCommand command = new SqlCommand("Sales.SP_GetOrderDetailsViewModleByOrderId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.Int) { Value = orderId });

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new OrderDetailsViewModel
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                    TotalAmoumt = reader.GetDecimal(reader.GetOrdinal("TotalAmoumt")),
                    Status = reader.GetByte(reader.GetOrdinal("Status")),
                    EstimatedDelivery = reader.GetDateTime(reader.GetOrdinal("EstimatedDelivery")),
                    ZipCode = reader.GetString(reader.GetOrdinal("ZipCode")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    City = reader.GetString(reader.GetOrdinal("City")),
                    CountryName = reader.GetString(reader.GetOrdinal("CountryName")),
                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                    Email = reader.GetString(reader.GetOrdinal("Email"))
                    
                };
            }
            return null;
        }


    }
}
