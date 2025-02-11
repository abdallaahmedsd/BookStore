﻿using BookStore.BusinessLogic.Interfaces;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Customer.Cart;
using BookStore.Utilties.BusinessHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.Services
{
    /// <summary>
    /// Provides services for managing shopping carts.
    /// </summary>
    public class ShoppingCartServices
    {
        private readonly ShoppingCardRepository _shoppingCartRepository;

        public ShoppingCartServices()
        {
            _shoppingCartRepository = new ShoppingCardRepository(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Finds a shopping cart item by its ID.
        /// </summary>
        /// <param name="id">The ID of the shopping cart item.</param>
        /// <returns>The shopping cart item if found; otherwise, null.</returns>
        public async Task<ShoppingCard?> FindAsync(int id)
        {
            if (id <= 0)
                return null;

            return await _shoppingCartRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Checks if a shopping cart item exists by its ID.
        /// </summary>
        /// <param name="id">The ID of the shopping cart item.</param>
        /// <returns>True if the item exists; otherwise, false.</returns>
        public async Task<bool> IsExistAsync(int id)
        {
            if (id <= 0)
                return false;

            return await _shoppingCartRepository.IsExistsAsync(id);
        }

        /// <summary>
        /// Gets all shopping cart items for a specific customer with pagination.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <param name="pageNumber">The page number for pagination.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A collection of shopping cart items.</returns>
        public async Task<IEnumerable<ShoppingCard>> GetShoppingCartByUserIdWithPaginationAsync(int customerId, int pageNumber = 1, int pageSize = 10)
        {
            if (customerId <= 0)
                throw new ArgumentOutOfRangeException(nameof(customerId), "Customer ID must be greater than zero.");
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than zero.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");

            return await _shoppingCartRepository.GetShoppingCartByUserIdWithPaginationAsync(customerId, pageNumber, pageSize);
        }

        /// <summary>
        /// Deletes a shopping cart item by its ID and customer ID.
        /// </summary>
        /// <param name="id">The ID of the shopping cart item.</param>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>True if the item was deleted; otherwise, false.</returns>
        public async Task<bool> DeleteAsync(int id, int customerId)
        {
            if (id <= 0 || customerId <= 0)
                return false;

            return await _shoppingCartRepository.DeleteAsync(id, customerId);
        }

        /// <summary>
        /// Deletes all shopping cart items for a specific customer.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>True if the items were deleted; otherwise, false.</returns>
        public async Task<bool> DeleteCustomerItemsAsync(int customerId)
        {
            if (customerId <= 0)
                return false;

            return await _shoppingCartRepository.DeleteCustomerItemsAsync(customerId);
        }

        /// <summary>
        /// Adds a new shopping cart item asynchronously.
        /// </summary>
        /// <param name="shoppingCart">The shopping cart item to add.</param>
        /// <returns>True if the item was added successfully; otherwise, false.</returns>
        public async Task<bool> AddAsync(ShoppingCard shoppingCart)
        {
            if (shoppingCart == null)
                throw new ArgumentNullException(nameof(shoppingCart));
            if (shoppingCart.BookID <= 0 || shoppingCart.Quantity <= 0 || shoppingCart.UserID <= 0)
                throw new ArgumentException("Invalid shopping cart item properties.");

            ShoppingCard? addedCart = await _shoppingCartRepository.InsertAsync(shoppingCart);
            return addedCart?.Id > 0;
        }

        /// <summary>
        /// Updates an existing shopping cart item asynchronously.
        /// </summary>
        /// <param name="shoppingCart">The shopping cart item to update.</param>
        /// <returns>True if the item was updated successfully; otherwise, false.</returns>
        public async Task<bool> UpdateAsync(ShoppingCard shoppingCart)
        {
            if (shoppingCart == null)
                throw new ArgumentNullException(nameof(shoppingCart));

            return await _shoppingCartRepository.UpdateAsync(shoppingCart);
        }

        /// <summary>
        /// Retrieves the shopping card items for a specific user by their user ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable list of shopping cards, or null if the user ID is invalid.</returns>
        public async Task<IEnumerable<ShoppingCard>?> GetShoppingCardByUserIDAsync(int userId)
        {
            if (userId <= 0) return null;

            return await _shoppingCartRepository.GetShoppingCardByUserIDAsync(userId);
        }

        /// <summary>
        /// Retrieves a shopping card item for a specific user and book ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="bookId">The book ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a shopping card, or null if the user ID is invalid.</returns>
        public async Task<ShoppingCard?> GetShoppingCardByUserIDandBookIdAsync(int userId, int bookId)
        {
            if (userId <= 0) return null;

            return await _shoppingCartRepository.GetShoppingCardByUserIDandBookIdAsync(userId, bookId);
        }

        /// <summary>
        /// Retrieves the count of shopping items for a specific user by their user ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the count of shopping items, or null if the user ID is invalid.</returns>
        public async Task<int?> GetShoppingItemsCountByUserIdAsync(int userId)
        {
            if (userId <= 0) return null;

            return await _shoppingCartRepository.GetShoppingItemsCountByUserIdAsync(userId);
        }

        /// <summary>
        /// Retrieves the shopping cart view model for a given user asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier for the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an IEnumerable of CartViewModel if the user ID is valid; otherwise, null.</returns>
        public async Task<IEnumerable<CartViewModel>?> GetShoppingCartViewModelAsync(int userId)
        {
            if (userId <= 0) return null;

            return await _shoppingCartRepository.GetShoppingCartViewModelAsync(userId);
        }

        /// <summary>
        /// Checks if the user has items in their shopping cart asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier for the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the user has items in their shopping cart.</returns>
        public async Task<bool> DoesUserHaveShoppingCartItemsAsync(int userId)
        {
            if (userId <= 0) return false;

            return await _shoppingCartRepository.DoesUserHaveShoppingCartItemsAsync(userId);
        }

    }
}
