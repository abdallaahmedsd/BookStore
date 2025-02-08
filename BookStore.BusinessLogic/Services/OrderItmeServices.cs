using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using BookStore.Models.Entities;
using BookStore.DataAccess.Repositories;
using BookStore.Utilties.BusinessHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Diagnostics.Eventing.Reader;

namespace BookstoreBackend.BLL.Services
{
    /// <summary>
    /// Service class for managing order items.
    /// </summary>
    public class OrderItmeServices
    {
   
     

        private static readonly OrderItemRepositry _orderitemrepo;

   
        static OrderItmeServices()
        {
            _orderitemrepo = new OrderItemRepositry(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Retrieves all order items for a given order ID.
        /// </summary>
        /// <param name="OrderId">The order ID.</param>
        /// <returns>A collection of order items.</returns>
        public async Task<IEnumerable<OrderItem>> GetAllItmesByOrderId(int OrderId)
        {
            var orderItmess = await _orderitemrepo.GetAllAsync(OrderId);
            return orderItmess;
        }


        /// <summary>
        /// Deletes the current order item.
        /// </summary>
        /// <returns>True if deletion was successful, otherwise false.</returns>
        public async Task<bool> DeleteAsync(int Id)
        {
            return await _orderitemrepo.Delete(Id);
        }

        /// <summary>
        /// Adds a new order item asynchronously.
        /// </summary>
        /// <returns>True if the item was added successfully, otherwise false.</returns>
        public async Task<bool> _AddAsync(OrderItem orderItem)
        {
            OrderItem? NeworderItem = await _orderitemrepo.InsertAsync(orderItem);

            if (NeworderItem != null) { orderItem.Id = NeworderItem.Id; return true; }
            else return false;
        }

        /// <summary>
        /// Updates an existing order item asynchronously.
        /// </summary>
        /// <returns>True if the update was successful, otherwise false.</returns>
        public async Task<bool> _UpdateAsync(OrderItem orderItem)
        {
        
            return await _orderitemrepo.UpdateAsync(orderItem);
        }

    }
}
