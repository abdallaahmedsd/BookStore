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
    public class ShippingServices
    {
        private readonly ShippingRepository _shippingRepository;

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
        /// Initializes a new instance of the <see cref="ShippingServices"/> class.
        /// </summary>
        public ShippingServices()
        {
            _shippingRepository = new ShippingRepository(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Finds a shipping service by order ID.
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the shipping service if found, otherwise null.</returns>
        public async Task<Shipping?> FindByOrderIdAsync(int orderId)
        {
            if (orderId <= 0) return null;
            Shipping? shipping = await _shippingRepository.GetShippingByOrderIDAsync(orderId);
            return shipping;
        }

        /// <summary>
        /// Finds a shipping service by ID.
        /// </summary>
        /// <param name="Id">The ID of the shipping.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the shipping service if found, otherwise null.</returns>
        public async Task<Shipping?> FindAsync(int Id)
        {
            if (Id <= 0) return null;
            Shipping? shipping = await _shippingRepository.GetByIdAsync(Id);
            return shipping;
        }

        /// <summary>
        /// Checks if a shipping exists by ID.
        /// </summary>
        /// <param name="Id">The ID of the shipping.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the shipping exists.</returns>
        public async Task<bool> IsExist(int Id)
        {
            if (Id <= 0) return false;
            string isExist = (await _shippingRepository.IsExistsAsync(Id)).ToString();
            return bool.Parse(isExist);
        }

        /// <summary>
        /// Updates the status of a shipping.
        /// </summary>
        /// <param name="shippingId">The ID of the shipping.</param>
        /// <param name="status">The new status.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the update was successful.</returns>
        public async Task<bool> UpdateStatus(int shippingId, enShippingStatus status)
        {
            if (shippingId <= 0) return false;

            return await _shippingRepository.UpdateShippingStatusAsync(shippingId, (byte)status);
        }

        /// <summary>
        /// Retrieves all shippings by user ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of shippings.</returns>
        public async Task<IEnumerable<Shipping>?> GetAllByUserId(int userId)
        {
            if (userId <= 0) return Enumerable.Empty<Shipping>();

            IEnumerable<Shipping>? shippings = await _shippingRepository.GetShippingsByUserId(userId);

            return shippings;
        }

        /// <summary>
        /// Adds a new shipping.
        /// </summary>
        /// <param name="newShipping">The new shipping entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the addition was successful.</returns>
        public async Task<bool> AddAsync(Shipping newShipping)
        {
            if (newShipping == null) return false;

            Shipping? shipping = await _shippingRepository.InsertAsync(newShipping);

            if (shipping == null) return false;

            newShipping.Id = shipping.Id;
            return newShipping.Id > 0;
        }
    }
}
