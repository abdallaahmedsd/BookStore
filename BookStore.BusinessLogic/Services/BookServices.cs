using BookStore.BusinessLogic.Interfaces;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels;
using BookStore.Utilties.BusinessHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.Services
{

    /// <summary>
    /// Provides services for managing books.
    /// </summary>
    public class BookServices : IServices
    {
        /// <summary>
        /// Enumeration for mode (Add or Update).
        /// </summary>
        private enum enMode { Add, Update }

        /// <summary>
        /// Current mode of the service (Add or Update).
        /// </summary>
        private enMode Mode = enMode.Add;


        /// <summary>
        /// Asynchronously gets a BookDTO object based on the current book's properties.
        /// </summary>
        //public Task<BookDTO> BDTOAsync => GetBookDTOAsync();

        /// <summary>
        /// Repository for accessing book data.
        /// </summary>
        private static readonly BookRepository _bookRepository;

        /// <summary>
        /// Gets or sets the ID of the book.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the ISBN of the book.
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Gets or sets the language ID of the book.
        /// </summary>
        public int LanguageID { get; set; }

        /// <summary>
        /// Gets or sets the author ID of the book.
        /// </summary>
        public int AuthorID { get; set; }

        /// <summary>
        /// Gets or sets the category ID of the book.
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Gets or sets the published date of the book.
        /// </summary>
        public DateTime PublishedDate { get; set; }

        /// <summary>
        /// Gets or sets the price of the book.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the description of the book.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who updated the book.
        /// </summary>
        public int? UpdatedByUserID { get; set; }

        /// <summary>
        /// Gets or sets the image path of the book.
        /// </summary>
        public string? ImagePath { get; set; }

        /// <summary>
        /// Gets the data transfer object for the current book.
        /// </summary>
       // public BookDTO BDTO { get; private set; }

        /// <summary>
        /// Static constructor to initialize the book repository.
        /// </summary>
        static BookServices()
        {
            _bookRepository = new BookRepository(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookServices"/> class.
        /// </summary>
        public BookServices()
        {

            Mode = enMode.Add;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookServices"/> class with the specified book and mode.
        /// </summary>
        /// <param name="book">The book entity.</param>
        /// <param name="mode">The mode (Add or Update).</param>
        private BookServices(Book book, enMode mode = enMode.Update)
        {
            Id = book.Id;
            Title = book.Title;
            ISBN = book.ISBA;
            LanguageID = book.LanguageID;
            Price = book.Price;
            CategoryID = book.CategoryID;
            PublishedDate = book.PublicationDate;
            Description = book.Description;
            ImagePath = book.CoverImage;
            AuthorID = book.AuthorID;
            UpdatedByUserID = book.UpdatedByUserID;
            Mode = mode;
        }

        /// <summary>
        /// Finds a book by ID asynchronously.
        /// </summary>
        /// <param name="id">The book ID.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the <see cref="BookServices"/> instance.</returns>
        public static async Task<BookServices?> FindAsync(int id)
        {
            if (id <= 0) return null;
            Book? book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                return new BookServices(book);
            }
            return null;
        }

        /// <summary>
        /// Gets the top N best-selling books.
        /// </summary>
        /// <param name="topN">The number of top best-selling books to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of best-selling books.</returns>
        public static async Task<IEnumerable<BestSellingBookDTO>?> GetTopBestSellingBooksAsync(int topN)
        {
            if (topN <= 0)
            {
                // Handle the error, return null, or throw an exception based on your needs
                return null;
            }
            return await _bookRepository.GetTopBestSellingBooksAsync(topN);
        }

        /// <summary>
        /// Gets the top N recently published books.
        /// </summary>
        /// <param name="topN">The number of top recently published books to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of recently published books.</returns>
        public static async Task<IEnumerable<RecentlyPublishedBookDTO>?> GetTopRecentlyPublishedBooksAsync(int topN)
        {
            if (topN <= 0)
            {
                // Handle the error, return null, or throw an exception based on your needs
                return null;
            }
            return await _bookRepository.GetTopRecentlyPublishedBooksAsync(topN);
        }

        /// <summary>
        /// Asynchronously retrieves additional data and constructs a BookDTO object.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains the BookDTO object with the current book's details, 
        /// including the category name and author name.
        /// </returns>
        //public async Task<BookDTO> GetBookDTOAsync()
        //{
        //    var categoryTask = CategoryServices.GetCategoryNameAsync(this.CategoryID);
        //    var authorTask =  GetAuthorNameAsync(this.AuthorID);

        //    await Task.WhenAll(categoryTask, authorTask);

        //    string categoryName = categoryTask.Result;
        //    string authorName = authorTask.Result;

        //    return new BookDTO(this.Id, this.Title, authorName, this.ISBN, categoryName, this.Price);
        //}


        /// <summary>
        /// Finds a book by title asynchronously.
        /// </summary>
        /// <param name="title">The book title.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the <see cref="BookServices"/> instance.</returns>
        public static async Task<BookServices?> FindAsync(string title)
        {
            if (string.IsNullOrEmpty(title)) return null;

            Book? book = await _bookRepository.GetByTitleAsync(title);
            if (book != null)
            {
                return new BookServices(book);
            }
            return null;
        }

        /// <summary>
        /// Finds a book by ISBN asynchronously.
        /// </summary>
        /// <param name="isbn">The book ISBN.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the <see cref="BookServices"/> instance.</returns>
        public static async Task<BookServices?> FindAsyncByISBN(string isbn)
        {
            if (string.IsNullOrEmpty(isbn)) return null;
            Book? book = await _bookRepository.GetByISBAAsync(isbn);
            if (book != null)
            {
                return new BookServices(book);
            }
            return null;
        }

        /// <summary>
        /// Creates an instance of <see cref="BookServices"/> asynchronously.
        /// </summary>
        /// <param name="book">The book entity.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the <see cref="BookServices"/> instance.</returns>
        //private static async Task<BookServices?> CreateBookServicesAsync(Book book)
        //{
        //    var categoryTask = GetCategoryNameAsync(book.CategoryID);
        //    var authorTask = GetAuthorNameAsync(book.AuthorID);

        //    await Task.WhenAll(categoryTask, authorTask);

        //    string categoryName = await categoryTask;
        //    string authorName = await authorTask;

        //    if (categoryName == null)
        //    {
        //        // Handle the error, return null, or throw an exception based on your needs
        //        return null;
        //    }

        //    BookServices bookServices = new BookServices(book);
        //    bookServices.BDTO = new BookDTO(book.Id, book.Title, authorName, book.ISBA, categoryName, book.Price);

        //    return bookServices;
        //}

        /// <summary>
        /// Gets the category name asynchronously.
        /// </summary>
        /// <param name="categoryId">The category ID.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the category name.</returns>
        private static async Task<string?> GetCategoryNameAsync(int categoryId)
        {
            if (categoryId <= 0) return null;

            CategoryServices? category = await CategoryServices.FindAsync(categoryId);
            return category?.Name ?? null;
        }

        /// <summary>
        /// Gets the author name asynchronously.
        /// </summary>
        /// <param name="authorId">The author ID.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the author name.</returns>
        private static async Task<string?> GetAuthorNameAsync(int authorId)
        {
            if (authorId <= 0) return null;
            // Simulate async retrieval from database or other service
            return await Task.FromResult("Author Name");
        }

        /// <summary>
        /// Gets all books by category ID asynchronously.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>A task representing the asynchronous operation. The task result is an enumerable collection of <see cref="BookDTO"/> objects.</returns>
        public static async Task<IEnumerable<BookDTO>?> GetAllByCategoryID(int id)
        {
            if (id <= 0) return null;
            IEnumerable<Book> books = await _bookRepository.GetAllByCategoryAsync(id);
            var bookDTOs = await Task.WhenAll(books.Select(async book => new BookDTO(book.Id, book.Title, await GetAuthorNameAsync(book.AuthorID), book.ISBA, await GetCategoryNameAsync(book.CategoryID), book.Price)
            ));
            return bookDTOs;
        }

        /// <summary>
        /// Retrieves all books by a specific author ID asynchronously.
        /// </summary>
        /// <param name="id">The author ID.</param>
        /// <returns>A collection of book DTOs.</returns>
        public static async Task<IEnumerable<BookDTO>?> GetAllByAuthorID(int id)
        {
            if (id <= 0) return null;

            IEnumerable<Book> books = await _bookRepository.GetAllByAuthorAsync(id);
            var bookDTOs = await Task.WhenAll(books.Select(async book =>
                new BookDTO(book.Id, book.Title, await GetAuthorNameAsync(book.AuthorID), book.ISBA, await GetCategoryNameAsync(book.CategoryID), book.Price)
            ));
            return bookDTOs;
        }

        /// <summary>
        /// Retrieves all books asynchronously.
        /// </summary>
        /// <returns>A collection of book DTOs.</returns>
        public static async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            IEnumerable<Book> books = await _bookRepository.GetAllAsync();
            var bookDTOs = await Task.WhenAll(books.Select(async book =>
                new BookDTO(book.Id, book.Title, await GetAuthorNameAsync(book.AuthorID), book.ISBA, await GetCategoryNameAsync(book.CategoryID), book.Price)
            ));
            return bookDTOs;
        }

        public static async Task<bool> IsExist(int Id)
        {
            if (Id <= 0) return false;

            return await _bookRepository.IsExistsAsync(Id);
        }

        /// <summary>
        /// Deletes a book by its ID.
        /// </summary>
        /// <param name="Id">The book ID.</param>
        /// <returns>True if the book was deleted; otherwise, false.</returns>
        public static async Task<bool> DeleteAsync(int Id)
        {
            if(Id <= 0) return false;
            return await _bookRepository.Delete(Id);
        }

        /// <summary>
        /// Adds a new book asynchronously.
        /// </summary>
        /// <returns>True if the book was added; otherwise, false.</returns>
        private async Task<bool> _AddAsync()
        {
            Book? book = await _bookRepository.InsertAsync(new Book
            {
                Title = this.Title,
                ISBA = this.ISBN,
                LanguageID = this.LanguageID,
                Price = this.Price,
                CategoryID = this.CategoryID,
                PublicationDate = this.PublishedDate,
                Description = this.Description,
                CoverImage =this.ImagePath,
                AuthorID = this.AuthorID,
                UpdatedByUserID = null
            });
            this.Id = book?.Id ?? 0;
            return this.Id > 0;
        }

        /// <summary>
        /// Updates an existing book asynchronously.
        /// </summary>
        /// <returns>True if the book was updated; otherwise, false.</returns>
        private async Task<bool> _UpdateAsync()
        {
            if (UpdatedByUserID == null)
            {
                // Handle the error, return false, or throw an exception based on your needs
                throw new Exception("UpdatedByUserID is required.");
            }

            return await _bookRepository.UpdateAsync(new Book
            {
                Id = this.Id,
                Title = this.Title,
                ISBA = this.ISBN,
                LanguageID = this.LanguageID,
                Price = this.Price,
                CategoryID = this.CategoryID,
                PublicationDate = this.PublishedDate,
                Description = this.Description,
                CoverImage = this.ImagePath,
                AuthorID = this.AuthorID,
                UpdatedByUserID = this.UpdatedByUserID
            });
        }

        /// <summary>
        /// Saves the book asynchronously, either by adding or updating it based on the mode.
        /// </summary>
        /// <returns>True if the book was saved; otherwise, false.</returns>
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
                    return false;
                case enMode.Update:
                    return await _UpdateAsync();
                default:
                    return false;
            }

        }
    }
}