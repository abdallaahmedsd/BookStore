using BookstoreBackend.DAL.Configurations;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using RepoSP.Net.Services;
using BookStore.Models.ViewModels.Customer.OrderVM;

namespace BookStore.DataAccess.Repositories
{
    public class OrderItemRepositry : StoredProcedureRepository<OrderItem>
    {
        private readonly string _connectionString;
        private readonly OrderItemsStoredProcConfiguration _config;

        public OrderItemRepositry(string connectionString) : base(connectionString, OrderItemsStoredProcConfiguration.Instance)
        {
            _connectionString = connectionString;
            _config = OrderItemsStoredProcConfiguration.Instance;
        }

        /// <summary>
        /// Get All a Order Item entity. This method is not supported in this repository and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override Task<IEnumerable<OrderItem>> GetAllAsync()
        {

            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a Order Item entity. This method is not supported in this repository and will always throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="entity">The Entity of the Order Item entity to Update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Thrown always to indicate that this method is not supported.</exception>
        [Obsolete("This method is not supported in this repository.", error: true)]
        public override Task<bool> UpdateAsync(OrderItem entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync(int OrderId)
        {
            var listOfItem = new List<OrderItem>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(_config.GetAllItemsByOrderId, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OrderId", OrderId);

                        connection.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                listOfItem.Add(_config.MapEntity(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return listOfItem;
        }

        public async Task<IEnumerable<OrderItemViewModel>> GetOrderItemViewModelAsync()
        {
            List<OrderItemViewModel> collection = new List<OrderItemViewModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("Sales.SP_GetOrderItemViewModel", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                OrderItemViewModel orderItemViewModler =  new OrderItemViewModel
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    BookID = reader.GetInt32(reader.GetOrdinal("BookID")),
                                    BookCoverImage = reader.IsDBNull(reader.GetOrdinal("CoverImage")) ? string.Empty : reader.GetString(reader.GetOrdinal("CoverImage")),
                                    BookTitle = reader.GetString(reader.GetOrdinal("Title")),
                                    BookPrice = reader.GetDecimal(reader.GetOrdinal("Price")),
                                    SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID"))
                                };

                                collection.Add(orderItemViewModler);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (logging, rethrowing, etc.)
                throw new ApplicationException("An error occurred while retrieving the order item.");
            }
            return collection;
        }

    } 
}