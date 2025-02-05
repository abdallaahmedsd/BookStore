using BookStore.BusinessLogic.Interfaces;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.ViewModels.Customer.Book;
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
            if (newBook == null) return false;
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
            
            if (book == null || book.UpdatedByUserID == null) return false;

            return await _bookRepository.UpdateAsync(book);
        }


        /// <summary>
        /// Retrieves the details of a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <returns>A <see cref="BookDetailsViewModel"/> instance if found; otherwise, null.</returns>
        public async Task<BookDetailsViewModel?> GetBookDetailsByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            return await _bookRepository.GetBookDetailsByIdAsync(id);
        }

        /// <summary>
        /// Retrieves the list of books for admin.
        /// </summary>
        /// <returns>A list of <see cref="BookListViewModel"/> instances.</returns>
        public async Task<List<BookListViewModel>> GetBookListAsync()
        {
            return await _bookRepository.GetBookListAsync();
        }

        /// <summary>
        /// Retrieves the top N best-selling books.
        /// </summary>
        /// <param name="topN">The number of top best-selling books to retrieve.</param>
        /// <returns>A IEnumerable of <see cref="BookHomeBestSellingViewModel"/> instances.</returns>
        public async Task<IEnumerable<BookHomeBestSellingViewModel>?> GetTopBestSellingBooksAsync(int topN)
        {
            if (topN <= 0) return null;
               

            return await _bookRepository.GetTopBestSellingBooksAsync(topN);
        }

        /// <summary>
        /// Retrieves the last added books.
        /// </summary>
        /// <param name="topN">The number of most recently added books to retrieve.</param>
        /// <returns>A list of <see cref="BookHomeLastAddedViewModel"/> instances.</returns>
        public async Task<IEnumerable<BookHomeLastAddedViewModel>?> GetLastAddedBooksAsync(int topN)
        {
            if (topN <= 0) return Enumerable.Empty<BookHomeLastAddedViewModel>();
               

            return await _bookRepository.GetLastAddedBooksAsync(topN);
        }
    }
}

