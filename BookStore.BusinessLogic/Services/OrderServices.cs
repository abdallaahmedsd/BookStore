using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Utilties.BusinessHelpers;

namespace BookstoreBackend.BLL.Services
{
    /// <summary>
    /// Provides services for managing orders in the bookstore.
    /// </summary>
    public class OrderServices
    {
        /// <summary>
        /// Represents the possible statuses of an order.
        /// </summary>
        public enum enStatus { Progress = 1, Complete, Cancel }
    
        private enum enMode { Add, Update };
        private enMode _Mode;
        
        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the date the order was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// Gets or sets the total amount of the order.
        /// </summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        public byte Status { get; set; }
        
        /// <summary>
        /// Gets or sets the ID of the user who placed the order.
        /// </summary>
        public int UserID { get; set; }

        private static readonly OrderRepositry _orderrepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderServices"/> class with an existing order.
        /// </summary>
        /// <param name="order">The order entity.</param>
        public OrderServices(Order order)
        {
            Id = order.Id;
            CreatedDate = order.CreatedDate;
            TotalAmount = order.TotalAmoumt;
            Status = order.Status;
            UserID = order.UserID;
            _Mode = enMode.Update;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderServices"/> class.
        /// </summary>
        public OrderServices() 
        {
            Id = -1;
            CreatedDate = DateTime.MinValue;
            TotalAmount = 0;
            Status = 0;
            UserID = 0;
            _Mode = enMode.Add;
        }

        static OrderServices()
        {
            _orderrepo = new OrderRepositry(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Retrieves all orders asynchronously.
        /// </summary>
        /// <returns>A collection of <see cref="OrderServices"/>.</returns>
        public static async Task<IEnumerable<OrderServices>> GetOrders()
        {
            var orders = await _orderrepo.GetAllAsync();
            return orders.Select(order => new OrderServices(order)).ToList();
        }

        /// <summary>
        /// Deletes an order asynchronously by ID.
        /// </summary>
        /// <param name="Id">The order ID.</param>
        /// <returns>True if deletion was successful, otherwise false.</returns>
        public static async Task<bool> DeleteAsync(int Id)
        {
            return await _orderrepo.Delete(Id);
        }

        /// <summary>
        /// Deletes the current order asynchronously.
        /// </summary>
        /// <returns>True if deletion was successful, otherwise false.</returns>
        public async Task<bool> DeleteAsync()
        {
            return await DeleteAsync(this.Id);
        }

        private async Task<bool> _AddAsync()
        {
            if (this.TotalAmount < 0)
                throw new ArgumentException("Total amount must be a non-negative value.");

            if (this.Status < 1 || this.Status > 3)
                throw new ArgumentException("Invalid status value. Use 1 (Progress), 2 (Complete), or 3 (Cancel). ");

            if (this.UserID <= 0)
                throw new ArgumentException("Invalid User ID.");

            Order? order = await _orderrepo.InsertAsync(new Order { Status = this.Status, UserID = this.UserID, TotalAmoumt = this.TotalAmount });

            this.Id = (order == null || order.Id <= 0) ? -1 : order.Id;
            return this.Id > 0;
        }

        private async Task<bool> _UpdateAsync()
        {
            if (!await _orderrepo.IsExistsAsync(this.Id))
                throw new ArgumentException("Id does not exist.");

            if (this.TotalAmount < 0)
                throw new ArgumentException("Total amount must be a non-negative value.");

            if (this.Status < 1 || this.Status > 3)
                throw new ArgumentException("Invalid status value. Use 1 (Progress), 2 (Complete), or 3 (Cancel). ");

            return await _orderrepo.UpdateAsync(new Order { Id = this.Id, Status = this.Status, UserID = this.UserID, TotalAmoumt = this.TotalAmount });
        }

        /// <summary>
        /// Saves the order asynchronously. Adds or updates the order based on the mode.
        /// </summary>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        public async Task<bool> SaveAsync()
        {
            switch (this._Mode)
            {
                case enMode.Add:
                    if (await _AddAsync())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;
                
                case enMode.Update:
                    return await _UpdateAsync();
                
                default:
                    return false;
            }
        }
    }
}
