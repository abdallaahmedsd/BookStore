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
    /// Provides services for managing payments.
    /// </summary>
    public class PaymentServices : IServices
    {
        private readonly static PaymentRepository _paymentRepository;

        /// <summary>
        /// Enumeration for mode (Add or Update).
        /// </summary>
        public enum enMode { Add = 0, Update = 1 }

        /// <summary>
        /// Current mode of the service (Add or Update).
        /// </summary>
        public enMode Mode = enMode.Add;

        /// <summary>
        /// Payment ID.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Order ID associated with the payment.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Payment date.
        /// </summary>
        public DateTime PaymentDate { get; private set; }

        /// <summary>
        /// Amount of the payment.
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// User ID who made the payment.
        /// </summary>
        public int UserID { get; private set; }

        /// <summary>
        /// Static constructor to initialize the payment repository.
        /// </summary>
        static PaymentServices()
        {
            _paymentRepository = new PaymentRepository(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentServices"/> class.
        /// </summary>
        public PaymentServices()
        {
            Mode = enMode.Add;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentServices"/> class with the specified payment and mode.
        /// </summary>
        /// <param name="payment">The payment entity.</param>
        /// <param name="mode">The mode (Add or Update).</param>
        private PaymentServices(Payment payment, enMode mode = enMode.Update)
        {
            this.Id = payment.Id;
            this.OrderId = payment.OrderId;
            this.PaymentDate = payment.PaymentDate;
            this.UserID = payment.UserId;
            this.Amount = payment.Amount;
            Mode = mode;
        }

        /// <summary>
        /// Retrieves all payments for a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result is an enumerable collection of Payment entities if found; otherwise, null.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the user ID is less than or equal to zero.</exception>
        public static async Task<IEnumerable<Payment>?> GetPaymentsByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero.");
            }
            IEnumerable<Payment>? payments = await _paymentRepository.GetPaymentsByUserIDAsync(userId);
            return payments;
        }

        /// <summary>
        /// Retrieves the payment details for a specific order asynchronously.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>A task representing the asynchronous operation. The task result is a PaymentServices instance if found; otherwise, null.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the order ID is less than or equal to zero.</exception>
        public static async Task<PaymentServices?> GetPaymentByOrderIdAsync(int orderId)
        {
            if (orderId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(orderId), "Order ID must be greater than zero.");
            }

            Payment? payment = await _paymentRepository.GetPaymentByOrderIDAsync(orderId);

            if (payment == null) return null;

            return new PaymentServices(payment, enMode.Update);
        }

        /// <summary>
        /// Adds a new payment asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is true if the payment is added successfully; otherwise, false.</returns>
        private async Task<bool> _AddAsync()
        {
            Payment? payment = await _paymentRepository.InsertAsync(new Payment
            {
                OrderId = OrderId
            });
            this.Id = payment?.Id ?? 0;
            return this.Id > 0;
        }

        /// <summary>
        /// Saves the current payment asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is true if the payment is saved successfully; otherwise, false.</returns>
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
                    else
                        return false;
                case enMode.Update:
                    return false;
                default:
                    return false;
            }
        }
    }
}
