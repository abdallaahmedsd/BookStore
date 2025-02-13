using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Entities
{
    /// <summary>
    /// Represents an item within a customer's order in the bookstore.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the order item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the associated order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the book associated with this order item.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the book in this order item.
        /// </summary>
        public int Quntity { get; set; } // Typo: Should be "Quantity"

        /// <summary>
        /// Gets or sets the subtotal cost for this order item.
        /// </summary>
        public decimal SubTotal { get; set; }
    }
}
