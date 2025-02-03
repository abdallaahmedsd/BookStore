using BookStore.BusinessLogic.Interfaces;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Utilties.BusinessHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.Services
{
    /// <summary>
    /// Provides services for managing shopping cards.
    /// </summary>
    public class ShoppingCardServices : IServices
    {
        /// <summary>
        /// Represents the mode of operation (Add or Update).
        /// </summary>
        public enum enMode { Add = 0, Update = 1 }

        /// <summary>
        /// Current mode of the service (Add or Update).
        /// </summary>
        private enMode Mode = enMode.Add;

       // private static readonly string _connectionString = "Server=.;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;";

        private static readonly ShoppingCardRepository _shoppingCardRepository;

        /// <summary>
        /// Gets the unique identifier for the shopping card.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the user ID associated with the shopping card.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the book ID associated with the shopping card.
        /// </summary>
        public int BookID { get; set; }

        /// <summary>
        /// Gets or sets the quantity of books in the shopping card.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the subtotal amount for the shopping card.
        /// </summary>
        public decimal SubTotal { get; private set; }

        /// <summary>
        /// Initializes the <see cref="ShoppingCardServices"/> class.
        /// </summary>
        static ShoppingCardServices()
        {
            _shoppingCardRepository = new ShoppingCardRepository(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCardServices"/> class.
        /// </summary>
        public ShoppingCardServices()
        {
            Mode = enMode.Add;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCardServices"/> class with the specified shopping card and mode.
        /// </summary>
        /// <param name="shoppingCard">The shopping card entity.</param>
        /// <param name="mode">The mode of operation.</param>
        private ShoppingCardServices(ShoppingCard shoppingCard, enMode mode = enMode.Update)
        {
            Id = shoppingCard.Id;
            SubTotal = shoppingCard.SubTotal;
            Quantity = shoppingCard.Quantity;
            BookID = shoppingCard.BookID;
            UserID = shoppingCard.UserID;
            Mode = mode;
        }

        /// <summary>
        /// Finds a shopping card entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the shopping card entity.</param>
        /// <returns>The shopping card entity if found; otherwise, null.</returns>
        public static async Task<ShoppingCardServices?> FindAsync(int id)
        {
            if (id <= 0) return null;
            ShoppingCard? shoppingCard = await _shoppingCardRepository.GetByIdAsync(id);
            if(shoppingCard == null) return null;
            return new ShoppingCardServices(shoppingCard, enMode.Update);
        }

        /// <summary>
        /// Checks if a shopping card entity exists by its ID.
        /// </summary>
        /// <param name="id">The ID of the shopping card entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the entity exists.</returns>
        public static async Task<bool> IsExist(int id)
        {
            if (id <= 0) return false;
            return await _shoppingCardRepository.IsExistsAsync(id);
        }

        /// <summary>
        /// Gets all shopping card entities for a specific customer with pagination.
        /// </summary>
        /// <param name="CustomerID">The ID of the customer.</param>
        /// <param name="PageNumber">The page number for pagination.</param>
        /// <param name="PageSize">The number of items per page.</param>
        /// <returns>A collection of shopping card entities if found; otherwise, null.</returns>
        public static async Task<IEnumerable<ShoppingCard>?> GetAllAsync(int CustomerID, int PageNumber = 1, int PageSize = 10)
        {
            if (CustomerID <= 0 || PageNumber <= 0 || PageSize <= 0) return null;
            return await _shoppingCardRepository.GetAllAsync(CustomerID, PageNumber, PageSize);
        }

        /// <summary>
        /// Deletes a shopping card entity by its ID and customer ID.
        /// </summary>
        /// <param name="Id">The ID of the shopping card entity.</param>
        /// <param name="CustomerID">The ID of the customer.</param>
        /// <returns>A boolean value indicating whether the operation was successful.</returns>
        public static async Task<bool> DeleteAsync(int Id, int CustomerID)
        {
            if (Id <= 0 || CustomerID <= 0) return false;
            return await _shoppingCardRepository.DeleteAsync(Id, CustomerID);
        }

        /// <summary>
        /// Deletes all shopping card items for a specific customer.
        /// </summary>
        /// <param name="CustomerID">The ID of the customer.</param>
        /// <returns>A boolean value indicating whether the operation was successful.</returns>
        public static async Task<bool> DeleteCustomerItemsAsync(int CustomerID)
        {
            if (CustomerID <= 0) return false;
            return await _shoppingCardRepository.DeleteCustomerItemsAsync(CustomerID);
        }

        /// <summary>
        /// Adds a new shopping card entity.
        /// </summary>
        /// <returns>A boolean value indicating whether the operation was successful.</returns>
        private async Task<bool> _AddAsync()
        {
            if (BookID <= 0 || Quantity <= 0 || UserID <= 0) return false;
            ShoppingCard? shoppingCard = await _shoppingCardRepository.InsertAsync(new ShoppingCard
            {
                BookID = this.BookID,
                Quantity = this.Quantity,
                UserID = this.UserID
            });
            this.Id = shoppingCard?.Id ?? 0;
            return  this.Id > 0;
        }

        /// <summary>
        /// Saves the current state of the shopping card service.
        /// </summary>
        /// <returns>A boolean value indicating whether the operation was successful.</returns>
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
