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

      

        private static readonly OrderRepositry _orderrepo;

        static OrderServices()
        {
            _orderrepo = new OrderRepositry(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Retrieves all orders asynchronously.
        /// </summary>
        /// <returns>A collection of <see cref="Order"/>.</returns>
        public async Task<IEnumerable<Order>> GetOrders()
        {
            var orders = await _orderrepo.GetAllAsync();
            return orders;
        }

        /// <summary>
        /// Deletes the current order asynchronously.
        /// </summary>
        /// <returns>True if deletion was successful, otherwise false.</returns>
        public async Task<bool> DeleteAsync(int Id)
        {
            return await _orderrepo.Delete(Id);
        }

        public async Task<bool> _AddAsync(Order order)
        {
            
            Order? Neworder = await _orderrepo.InsertAsync(order);

            if (Neworder != null) { order.Id = Neworder.Id; return true; }

            else return false;
        }

        public async Task<bool> _UpdateAsync(Order order)
        {
            return await _orderrepo.UpdateAsync(order);
        }

    }
}
