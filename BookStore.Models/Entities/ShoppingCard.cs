using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Entities
{
    /// <summary>
    /// Represents a shopping card entity.
    /// </summary>
    public class ShoppingCard 
    {
        /// <summary>
        /// Gets or sets the unique identifier for the shopping card.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated book.
        /// </summary>
        public int BookID { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the book.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the subtotal value. The public setter is used to allow setting the subtotal value within the class,
        /// as it is calculated based on the business logic (e.g., quantity * price).
        /// </summary>
        public decimal SubTotal { get;  set; }

        /// <summary>
        /// Gets or sets the ID of the user associated with the shopping card.
        /// </summary>
        public int UserID { get; set; }
    }
}
