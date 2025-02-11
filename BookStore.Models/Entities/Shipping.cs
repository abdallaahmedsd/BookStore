using System;

namespace BookStore.Models.Entities
{
    /// <summary>
    /// Represents a shipping transaction for an order.
    /// </summary>
    public class Shipping
    {
        /// <summary>
        /// Gets or sets the unique identifier for the shipping.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the related order.
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// Gets or sets the shipping address.
        /// </summary>
        public string ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the shipping was initiated.
        /// </summary>
        public DateTime? ShippingDate { get; set; } 

        /// <summary>
        /// Gets or sets the tracking number for the shipment.
        /// </summary>
        public string? TrackingNumber { get; set; }

        /// <summary>
        /// Gets or sets the estimated delivery date and time.
        /// </summary>
        public DateTime? EstimatedDelivery { get; set; }

        /// <summary>
        /// Gets or sets the status of the shipping.
        /// </summary>
        public byte Status { get; set; }
    }
}
