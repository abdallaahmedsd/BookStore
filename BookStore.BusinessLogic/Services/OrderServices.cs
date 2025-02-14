using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Admin.Order;
using BookStore.Models.ViewModels.Customer.OrderVM;
using BookStore.Utilties.BusinessHelpers;
using static BookStore.BusinessLogic.Services.ShippingServices;

namespace BookstoreBackend.BLL.Services
{
    /// <summary>
    /// Provides services for managing orders in the bookstore.
    /// </summary>
    public class OrderServices
    {
        public enum enOrderStatus { Approved = 1, Process = 2, Shipped = 3, Cancel =4 }

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

   
        public async Task<bool> UpdateStatus(int OrderId, enOrderStatus status)
        {
            if (OrderId <= 0) return false;
            return await _orderrepo.UpdateOrderStatusAsync(OrderId, (byte)status);
        }


        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            if (id <= 0) return null;

            return await _orderrepo.GetByIdAsync(id);
        }


        public async Task<IEnumerable<OrderListViewModel>> GetOrderListViewModelAsync()
        {
            return await _orderrepo.GetOrderListViewModelAsync();
        }



        public async Task<OrderDetailsViewModel?> GetOrderDetailsViewModleByOrderId(int OrderID)
        {
            if (OrderID <= 0) return null;
            return await _orderrepo.GetOrderDetailsViewModleByOrderId(OrderID);
        }     
        
        
        public async Task<IEnumerable<OrderListForCustomerViewModel>> GetOrderListForCustomerViewModelByUserId(int userId)
        {
            if (userId <= 0) return null;
            return await _orderrepo.GetOrderListForCustomerViewModelByUserId(userId);
        }


    }
}
