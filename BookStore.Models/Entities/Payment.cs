using System;

namespace BookStore.Models.Entities
{
    /// <summary>
    /// Represents a payment transaction for an order.
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the related order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the payment was made.
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Gets or sets the amount of the payment.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user who made the payment.
        /// </summary>
        public int UserId { get; set; }
    }
}
