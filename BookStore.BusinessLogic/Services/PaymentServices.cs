using BookStore.BusinessLogic.Interfaces;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Utilties.BusinessHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.Services
{
    /// <summary>
    /// Provides services for managing payments.
    /// </summary>
    public class PaymentServices 
    {
        private readonly PaymentRepository _paymentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentServices"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository.</param>
        /// <exception cref="ArgumentNullException">Thrown when the payment repository is null.</exception>
        public PaymentServices()
        {
            _paymentRepository = new PaymentRepository(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Retrieves all payments for a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A collection of <see cref="Payment"/> entities.</returns>
        public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero.");

            return await _paymentRepository.GetPaymentsByUserIDAsync(userId);
        }

        /// <summary>
        /// Retrieves the payment details for a specific order asynchronously.
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        /// <returns>The <see cref="Payment"/> entity if found; otherwise, null.</returns>
        public async Task<Payment?> GetPaymentByOrderIdAsync(int orderId)
        {
            if (orderId <= 0)
                throw new ArgumentOutOfRangeException(nameof(orderId), "Order ID must be greater than zero.");

            return await _paymentRepository.GetPaymentByOrderIDAsync(orderId);
        }

        /// <summary>
        /// Adds a new payment asynchronously.
        /// </summary>
        /// <param name="newPayment">The new payment to add.</param>
        /// <returns>True if the payment is added successfully; otherwise, false.</returns>
        public async Task<bool> AddAsync(Payment newPayment)
        {
            if (newPayment == null)
                throw new ArgumentNullException(nameof(newPayment));

            Payment? payment = await _paymentRepository.InsertAsync(newPayment);
            newPayment.Id = payment?.Id ?? 0;
            return newPayment.Id > 0;
        }

        /// <summary>
        /// Checks if a payment exists by ID asynchronously.
        /// </summary>
        /// <param name="id">The payment ID.</param>
        /// <returns>True if the payment exists; otherwise, false.</returns>
        public async Task<bool> IsExistAsync(int id)
        {
            if (id <= 0)
                return false;

            return await _paymentRepository.IsExistsAsync(id);
        }


        public async Task<Payment?> FindAsync(int id)
        {
            if (id <= 0) return null;

            return await _paymentRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Updates an existing payment asynchronously.
        /// </summary>
        /// <param name="payment">The payment to update.</param>
        /// <returns>True if the payment is updated successfully; otherwise, false.</returns>
        public async Task<bool> UpdateAsync(Payment payment)
        {
            if (payment == null)
                return false;

            return await _paymentRepository.UpdateAsync(payment);
        }

        /// <summary>
        /// Deletes a payment by ID asynchronously.
        /// </summary>
        /// <param name="id">The payment ID.</param>
        /// <returns>True if the payment is deleted successfully; otherwise, false.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                return false;

            return await _paymentRepository.Delete(id);
        }


        /// <summary>
        /// Deletes payments based on UserID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        public async Task<bool> DeletePaymentByUserIdAsync(int userId)
        {
            if (userId <= 0) return false;

            return await _paymentRepository.DeletePaymentByUserIdAsync(userId);
        }
    }
}
