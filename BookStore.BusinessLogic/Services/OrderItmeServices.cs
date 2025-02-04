using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using BookStore.Models.Entities;
using BookStore.DataAccess.Repositories;
using BookStore.Utilties.BusinessHelpers;

namespace BookstoreBackend.BLL.Services
{
    /// <summary>
    /// Service class for managing order items.
    /// </summary>
    public class OrderItmeServices
    {
        private enum enMode { Add, Update };
        private enMode _Mode;

        /// <summary>
        /// Gets or sets the order item ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the order ID associated with this item.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the book ID associated with this order item.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the book in the order.
        /// </summary>
        public int Quntity { get; set; }

        /// <summary>
        /// Gets or sets the subtotal amount for this order item.
        /// </summary>
        public decimal SubTotal { get; set; }

        private static readonly OrderItemRepositry _orderitemrepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItmeServices"/> class with an existing order item.
        /// </summary>
        /// <param name="orderItem">The order item entity.</param>
        public OrderItmeServices(OrderItem orderItem)
        {
            Id = orderItem.Id;
            OrderId = orderItem.OrderId;
            BookId = orderItem.BookId;
            Quntity = orderItem.Quntity;
            SubTotal = orderItem.SubTotal;
            _Mode = enMode.Update;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItmeServices"/> class for adding a new order item.
        /// </summary>
        public OrderItmeServices()
        {
            Id = -1;
            OrderId = -1;
            BookId = -1;
            Quntity = -1;
            SubTotal = -1;
            _Mode = enMode.Add;
        }

        static OrderItmeServices()
        {
            _orderitemrepo = new OrderItemRepositry(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Retrieves all order items for a given order ID.
        /// </summary>
        /// <param name="OrderId">The order ID.</param>
        /// <returns>A collection of order items.</returns>
        public static async Task<IEnumerable<OrderItmeServices>> GetAllItmesByOrderId(int OrderId)
        {
            var orders = await _orderitemrepo.GetAllAsync(OrderId);
            return orders.Select(orderItme => new OrderItmeServices(orderItme)).ToList();
        }

        /// <summary>
        /// Deletes an order item by its ID.
        /// </summary>
        /// <param name="Id">The ID of the order item.</param>
        /// <returns>True if deletion was successful, otherwise false.</returns>
        public static async Task<bool> DeleteAsync(int Id)
        {
            return await _orderitemrepo.Delete(Id);
        }

        /// <summary>
        /// Deletes the current order item.
        /// </summary>
        /// <returns>True if deletion was successful, otherwise false.</returns>
        public async Task<bool> DeleteAsync()
        {
            return await DeleteAsync(this.Id);
        }

        /// <summary>
        /// Adds a new order item asynchronously.
        /// </summary>
        /// <returns>True if the item was added successfully, otherwise false.</returns>
        private async Task<bool> _AddAsync()
        {
            if (this.BookId <= 0)
                throw new ArgumentException("Invalid Book ID.");

            if (this.Quntity <= 0)
                throw new ArgumentException("Quantity should be greater than 0.");

            if (this.SubTotal < 0)
                throw new ArgumentException("Subtotal cannot be negative.");

            OrderItem? orderItem = await _orderitemrepo.InsertAsync(
                new OrderItem
                {
                    Id = this.Id,
                    BookId = this.BookId,
                    OrderId = this.OrderId,
                    Quntity = this.Quntity,
                    SubTotal = this.SubTotal
                });

            this.Id = (orderItem == null || orderItem.Id <= 0) ? -1 : orderItem.Id;
            return this.Id > 0;
        }

        /// <summary>
        /// Updates an existing order item asynchronously.
        /// </summary>
        /// <returns>True if the update was successful, otherwise false.</returns>
        private async Task<bool> _UpdateAsync()
        {
            if (await _orderitemrepo.IsExistsAsync(this.Id))
                throw new ArgumentException("The Order does not exist.");

            if (this.BookId != null && this.BookId <= 0)
                throw new ArgumentException("Invalid Book ID.");

            if (this.Quntity != null && this.Quntity <= 0)
                throw new ArgumentException("Quantity should be greater than 0.");

            if (this.SubTotal != null && this.SubTotal < 0)
                throw new ArgumentException("Subtotal cannot be negative.");

            return await _orderitemrepo.UpdateAsync(new OrderItem { Id = this.Id, BookId = this.BookId, OrderId = this.OrderId, Quntity = this.Quntity, SubTotal = this.SubTotal });
        }

        /// <summary>
        /// Saves the order item asynchronously by adding or updating it.
        /// </summary>
        /// <returns>True if the save operation was successful, otherwise false.</returns>
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
