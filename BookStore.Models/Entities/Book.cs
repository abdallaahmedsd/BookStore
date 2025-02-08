using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Entities
{
    /// <summary>
    /// Represents a book entity.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Gets or sets the unique identifier for the book.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the author of the book.
        /// </summary>
        public int AuthorID { get; set; }

        public Author? Author { get; set; }

        /// <summary>
        /// Gets or sets the ID of the category the book belongs to.
        /// </summary>
        public int CategoryID { get; set; }

        public Category? Category { get; set; }

        /// <summary>
        /// Gets or sets the ID of the language the book is written in.
        /// </summary>
        public int LanguageID { get; set; }

        public Language? Language { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the price of the book.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the description of the book.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the International Standard Book Number (ISBN) of the book.
        /// </summary>
        public string ISBA { get; set; }

        /// <summary>
        /// Gets or sets the publication date of the book.
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets the URL of the cover image of the book.
        /// </summary>
        public string? CoverImage { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who last updated the book's price record.
        /// </summary>
        public int? UpdatedByUserID { get; set; }
    }
}
