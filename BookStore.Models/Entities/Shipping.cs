using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        /// Gets or sets the country for the shipping address.
        /// </summary>
        public int CountryID { get; set; }

        /// <summary>
        /// Gets or sets the city for the shipping address.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the address for the shipping.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the zip code for the shipping address.
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the carrier for the shipment. This value is nullable.
        /// </summary>
        public string? Carrier { get; set; }
    }
}
