using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Entities
{
    /// <summary>
    /// Represents a customer's order in the bookstore system.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the unique identifier for the order.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the order was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the order.
        /// </summary>
        public decimal TotalAmoumt { get; set; } // Typo: Should be "TotalAmount"

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who placed the order.
        /// </summary>
        public int UserID { get; set; }
    }
}
