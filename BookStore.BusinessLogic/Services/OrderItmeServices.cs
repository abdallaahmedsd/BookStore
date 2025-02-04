using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using BookStore.Models.Entities;
using BookStore.DataAccess.Repositories;

namespace BookstoreBackend.BLL.Services
{
    public class OrderItmeServices
    {
        public enum enMode { Add, Update };
        private enMode _Mode;
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quntity { get; set; }
        public decimal SubTotal { get; set; }

        private static readonly OrderItemRepositry _orderitemrepo;

        private static readonly string _connectionString = "Server=.;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public OrderItmeServices(OrderItem orderItem)
        {
            Id = orderItem.Id;
            OrderId = orderItem.OrderId;
            BookId = orderItem.BookId;
            Quntity = orderItem.Quntity;
            SubTotal = orderItem.SubTotal;

            _Mode = enMode.Update;
        }

   

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
            _orderitemrepo= new OrderItemRepositry(_connectionString);
        }


        public static async Task<IEnumerable<OrderItmeServices>> GetAllItmesByOrderId(int OrderId)
        {
            var orders = await _orderitemrepo.GetAllAsync(OrderId);
            return orders.Select(orderItme => new OrderItmeServices(orderItme)).ToList();
        }

        public static async Task<bool> DeleteAsync(int Id)
        {
            return await _orderitemrepo.Delete(Id);
        }

        public async Task<bool> DeleteAsync()
        {
            return await DeleteAsync(this.Id);
        }

        private async Task<bool> _AddAsync()
        {


            if (this.BookId <= 0)
                throw new ArgumentException("Invalid Book ID.");

            if (this.Quntity <= 0)
                throw new ArgumentException("Quantity should be greater than 0.");


            // Check if Book Exists
            //if (!await _bookRepo.IsExistsAsync(this.BookId))
            //    throw new ArgumentException("The Book does not exist.");

            if (this.SubTotal < 0)
                throw new ArgumentException("Subtotal cannot be negative.");


        

            OrderItem? orderItem = await _orderitemrepo.InsertAsync(
                new OrderItem {
                    Id = this.Id, 
                    BookId = this.BookId, 
                    OrderId = this.OrderId, 
                    Quntity = this.Quntity, 
                    SubTotal = this.SubTotal 
                });

            this.Id = (orderItem == null || orderItem.Id <= 0) ? -1 : orderItem.Id; ;
            return orderItem?.Id > 0;

        }

        private async Task<bool> _UpdateAsync()
        {
            if (await _orderitemrepo.IsExistsAsync(this.Id))
                throw new ArgumentException("The Order does not exist.");

            if (this.BookId != null && this.BookId <= 0)
                throw new ArgumentException("Invalid Book ID.");

            if (this.Quntity != null && this.Quntity <= 0)
                throw new ArgumentException("Quantity should be greater than 0.");


            // Check if Book Exists
            //if (!await _bookRepo.IsExistsAsync(this.BookId))
            //    throw new ArgumentException("The Book does not exist.");

            if (this.SubTotal != null && this.SubTotal < 0)
                throw new ArgumentException("Subtotal cannot be negative.");



            return await _orderitemrepo.UpdateAsync(new OrderItem { Id = this.Id, BookId = this.BookId, OrderId = this.OrderId, Quntity = this.Quntity, SubTotal = this.SubTotal});
        }

        public async Task<bool> SaveAsync()
        {
            switch (this._Mode)
            {
                case enMode.Add:
                    {
                        if (await _AddAsync())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        return false;
                    }
                case enMode.Update:
                    return await _UpdateAsync();

                default:
                    return false;
            }


        }
    }
}
