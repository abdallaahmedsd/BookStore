using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    /// <summary>
    /// Represents a data transfer object for a book.
    /// </summary>
    public class BookDTO
    {
        /// <summary>
        /// Gets or sets the ID of the book.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the author of the book.
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// Gets or sets the ISBN of the book.
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Gets or sets the category of the book.
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets the price of the book.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Initializes a new instance of the BookDTO class.
        /// </summary>
        /// <param name="Id">The ID of the book.</param>
        /// <param name="Title">The title of the book.</param>
        /// <param name="Author">The author of the book.</param>
        /// <param name="ISBN">The ISBN of the book.</param>
        /// <param name="Category">The category of the book.</param>
        /// <param name="Price">The price of the book.</param>
        public BookDTO(int Id, string Title, string Author, string ISBN, string Category, decimal Price)
        {
            this.Id = Id;
            this.Title = Title;
            this.Author = Author;
            this.ISBN = ISBN;
            this.Category = Category;
            this.Price = Price;
        }
    }
}
