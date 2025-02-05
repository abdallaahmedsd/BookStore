using BookStore.BusinessLogic.Interfaces;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Utilties.BusinessHelpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.Services
{
    /// <summary>
    /// Provides services for managing shipping operations.
    /// </summary>
    public class ShippingServices : IServices
    {
        private readonly static ShippingRepository _shippingReository;

        /// <summary>
        /// Specifies the operation mode for shipping services.
        /// </summary>
        public enum enMode { Add, Update }

        /// <summary>
        /// Specifies the current operation mode.
        /// </summary>
        public enMode Mode = enMode.Add;

        /// <summary>
        /// Specifies the various statuses for shipping.
        /// </summary>
        public enum enShippingStatus
        {
            Ordered = 1,
            Packed = 2,
            InTransit = 3,
            Delivered = 4
        }

        /// <summary>
        /// Gets the unique identifier for the shipping.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the unique identifier for the related order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the shipping address.
        /// </summary>
        public string ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets the tracking number for the shipment.
        /// </summary>
        public string TrackingNumber { get; set; }

        /// <summary>
        /// Gets the date and time when the shipping was initiated.
        /// </summary>
        public DateTime ShippingDate { get; private set; }

        /// <summary>
        /// Gets or sets the estimated delivery date and time.
        /// </summary>
        public DateTime EstimatedDelivery { get; set; }

        /// <summary>
        /// Gets or sets the status of the shipping.
        /// </summary>
        public enShippingStatus Status { get; set; }

        /// <summary>
        /// Gets the status of the shipping as a string.
        /// </summary>
        public string StatusString => this.Status.ToString();

        static ShippingServices()
        {
            _shippingReository = new ShippingRepository(ConnectionConfig._connectionString);
        }

        public ShippingServices()
        {
            Status = enShippingStatus.Ordered;
            Mode = enMode.Add;
        }

        private ShippingServices(Shipping shipping, enMode mode = enMode.Update)
        {
            this.Id = shipping.Id;
            this.ShippingDate = shipping.ShippingDate;
            this.Status = (enShippingStatus)shipping.Status;
            this.EstimatedDelivery = shipping.EstimatedDelivery;
            this.OrderId = shipping.OrderID;
            this.TrackingNumber = shipping.TrackingNumber;
            this.ShippingAddress = shipping.ShippingAddress;
            Mode = mode;
        }

        /// <summary>
        /// Finds a shipping service by order ID.
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the shipping service if found, otherwise null.</returns>
        public static async Task<ShippingServices?> FindByOrderIdAsync(int orderId)
        {
            if (orderId <= 0) return null;
            Shipping? shipping = await _shippingReository.GetShippingByOrderIDAsync(orderId);
            if (shipping == null) return null;

            return new ShippingServices(shipping, enMode.Update);
        }

        /// <summary>
        /// Finds a shipping service by ID.
        /// </summary>
        /// <param name="Id">The ID of the shipping.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the shipping service if found, otherwise null.</returns>
        public static async Task<ShippingServices?> FindAsync(int Id)
        {
            if (Id <= 0) return null;
            Shipping? shipping = await _shippingReository.GetByIdAsync(Id);
            if (shipping == null) return null;

            return new ShippingServices(shipping, enMode.Update);
        }

        /// <summary>
        /// Checks if a shipping exists by ID.
        /// </summary>
        /// <param name="Id">The ID of the shipping.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the shipping exists.</returns>
        public static async Task<bool> IsExist(int Id)
        {
            if (Id <= 0) return false;

            return await _shippingReository.IsExistsAsync(Id);
        }

        /// <summary>
        /// Updates the status of a shipping.
        /// </summary>
        /// <param name="shippingId">The ID of the shipping.</param>
        /// <param name="status">The new status.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the update was successful.</returns>
        public static async Task<bool> UpdateStatus(int shippingId, enShippingStatus status)
        {
            return await _shippingReository.UpdateShippingStatusAsync(shippingId, (byte)status);
        }

        /// <summary>
        /// Retrieves all shippings by user ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of shippings.</returns>
        public static async Task<IEnumerable<Shipping?>> GetAllByUserId(int userId)
        {
            if (userId <= 0) return Enumerable.Empty<Shipping?>();

            return await _shippingReository.GetShippingsByUserId(userId);
        }

        private async Task<bool> _AddAsync()
        {
            Shipping? shipping = await _shippingReository.InsertAsync(new Shipping()
            {
                OrderID = this.OrderId,
                ShippingAddress = this.ShippingAddress,
                TrackingNumber = this.TrackingNumber,
                EstimatedDelivery = this.EstimatedDelivery
            });

            if (shipping == null) return false;
            this.Id = shipping?.Id ?? 0;
            this.Status = (enShippingStatus)shipping.Status;
            this.ShippingDate = shipping.ShippingDate;

            return this.Id > 0;
        }

        /// <summary>
        /// Saves the shipping service.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the save was successful.</returns>
        public async Task<bool> SaveAsync()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (await _AddAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return false;
                default:
                    return false;
            }
        }
    }
}
