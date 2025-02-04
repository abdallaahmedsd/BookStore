using BookStore.BusinessLogic.Interfaces;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels;
using BookStore.Utilties.BusinessHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.Services
{
    /// <summary>
    /// Provides services for managing books.
    /// </summary>
    public class BookServices 
    {
        private readonly BookRepository _bookRepository;

        public BookServices()
        {
            _bookRepository = new BookRepository(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Finds a book by ID asynchronously.
        /// </summary>
        /// <param name="id">The book ID.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the <see cref="Book"/> instance.</returns>
        public async Task<Book?> FindAsync(int id)
        {
            if (id <= 0) return null;
            return await _bookRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Gets the top N best-selling books.
        /// </summary>
        /// <param name="topN">The number of top best-selling books to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of best-selling books.</returns>
        public async Task<IEnumerable<BestSellingBookDTO>?> GetTopBestSellingBooksAsync(int topN)
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
        public async Task<IEnumerable<RecentlyPublishedBookDTO>?> GetTopRecentlyPublishedBooksAsync(int topN)
        {
            if (topN <= 0)
            {
                // Handle the error, return null, or throw an exception based on your needs
                return null;
            }
            return await _bookRepository.GetTopRecentlyPublishedBooksAsync(topN);
        }

        /// <summary>
        /// Finds a book by title asynchronously.
        /// </summary>
        /// <param name="title">The book title.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the <see cref="Book"/> instance.</returns>
        public async Task<Book?> FindAsync(string title)
        {
            if (string.IsNullOrEmpty(title)) return null;
            return await _bookRepository.GetByTitleAsync(title);
        }

        /// <summary>
        /// Finds a book by ISBN asynchronously.
        /// </summary>
        /// <param name="isbn">The book ISBN.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the <see cref="Book"/> instance.</returns>
        public async Task<Book?> FindAsyncByISBN(string isbn)
        {
            if (string.IsNullOrEmpty(isbn)) return null;
            return await _bookRepository.GetByISBNAsync(isbn);
        }

        public async Task<IEnumerable<Book>?> GetAllByCategoryID(int id)
        {
            if (id <= 0) return null;
            return await _bookRepository.GetAllByCategoryAsync(id);
        }

        /// <summary>
        /// Retrieves all books by a specific author ID asynchronously.
        /// </summary>
        /// <param name="id">The author ID.</param>
        /// <returns>A collection of books.</returns>
        public async Task<IEnumerable<Book>?> GetAllByAuthorID(int id)
        {
            if (id <= 0) return null;
            return await _bookRepository.GetAllByAuthorAsync(id);
        }

        /// <summary>
        /// Retrieves all books asynchronously.
        /// </summary>
        /// <returns>A collection of books.</returns>
        public async Task<IEnumerable<Book>?> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<bool> IsExist(int id)
        {
            if (id <= 0) return false;
            return await _bookRepository.IsExistsAsync(id);
        }

        /// <summary>
        /// Deletes a book by its ID.
        /// </summary>
        /// <param name="id">The book ID.</param>
        /// <returns>True if the book was deleted; otherwise, false.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0) return false;
            return await _bookRepository.Delete(id);
        }

        /// <summary>
        /// Adds a new book asynchronously.
        /// </summary>
        /// <param name="newBook">The new book to add.</param>
        /// <returns>True if the book was added; otherwise, false.</returns>
        public async Task<bool> AddAsync(Book newBook)
        {
            if (newBook == null) throw new ArgumentNullException(nameof(newBook));
            Book? book = await _bookRepository.InsertAsync(newBook);
            newBook.Id = book?.Id ?? 0;
            return newBook.Id > 0;
        }

        /// <summary>
        /// Updates an existing book asynchronously.
        /// </summary>
        /// <param name="book">The book to update.</param>
        /// <returns>True if the book was updated; otherwise, false.</returns>
        public async Task<bool> UpdateAsync(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (book.UpdatedByUserID == null)
            {
                // Handle the error, return false, or throw an exception based on your needs
                throw new Exception("UpdatedByUserID is required.");
            }
            return await _bookRepository.UpdateAsync(book);
        }
    }
}
