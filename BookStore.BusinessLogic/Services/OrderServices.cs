using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;


namespace BookstoreBackend.BLL.Services
{
    public class OrderServices
    {
        public enum enStatus { Progress = 1, Complete, Cancel }
    
        public enum enMode { Add, Update };
        private enMode _Mode;
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalAmoumt { get; set; }
        public byte Status { get; set; }
        public int UserID { get; set; }

        private static readonly OrderRepositry _orderrepo;

        private static readonly string _connectionString = "Server=.;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;";


        public OrderServices(Order order)
        {
            Id = order.Id;
            CreatedDate = order.CreatedDate;
            TotalAmoumt = order.TotalAmoumt;
            Status = order.Status;
            UserID = order.UserID;

            _Mode = enMode.Update;
        }

        public OrderServices() 
        {
            Id = -1;
            CreatedDate = DateTime.MinValue;
            TotalAmoumt = 0;
            Status = 0;
            UserID = 0;

            _Mode = enMode.Add;
        }

        static OrderServices()
        {
            _orderrepo = new OrderRepositry(_connectionString);
        }


        public static async Task<IEnumerable<OrderServices>> GetOrders()
        {
            var orders = await _orderrepo.GetAllAsync();
            return orders.Select(order => new OrderServices(order)).ToList();
        }

        public static async Task<bool> DeleteAsync(int Id)
        {
            return await _orderrepo.Delete(Id);
        }

        public async Task<bool> DeleteAsync()
        {
            return await DeleteAsync(this.Id);
        }



        private async Task<bool> _AddAsync()
        {

            if (this.TotalAmoumt < 0)
                throw new ArgumentException("Total amount must be a non-negative value.");

            if (this.Status < 1 || this.Status > 3)
                throw new ArgumentException("Invalid status value. Use 1 (Progress), 2 (Complete), or 3 (Cancel).");

            if (this.UserID <= 0)
                throw new ArgumentException("Invalid User ID.");


            Order? order = await _orderrepo.InsertAsync(new Order { Status = this.Status, UserID = this.UserID, TotalAmoumt = this.TotalAmoumt });

            this.Id = (order == null || order.Id <= 0) ? -1 : order.Id;
            return order?.Id > 0;

        }

        private async Task<bool> _UpdateAsync()
        {
            if (!await _orderrepo.IsExistsAsync(this.Id))
                throw new ArgumentException("Id Is Not Exists.");

            if (this.TotalAmoumt != null && this.TotalAmoumt < 0)
                throw new ArgumentException("Total amount must be a non-negative value.");

            if (this.Status != null && this.Status < 1 || this.Status > 3)
                throw new ArgumentException("Invalid status value. Use 1 (Progress), 2 (Complete), or 3 (Cancel).");

           
            return await _orderrepo.UpdateAsync(new Order {Id = this.Id, Status = this.Status, UserID = this.UserID, TotalAmoumt = this.TotalAmoumt });
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
