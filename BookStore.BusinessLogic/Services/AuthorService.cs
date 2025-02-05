using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Admin;
using BookStore.Utilties.BusinessHelpers;
using Microsoft.Identity.Client;

namespace BookstoreBackend.BLL.Services
{
    /// <summary>
    /// Service class for managing authors in the bookstore system.
    /// </summary>
    public class AuthorService
    {


        private static readonly AuthorRepository _authorrepo;


        static AuthorService()
        {
            _authorrepo = new AuthorRepository(ConnectionConfig._connectionString);
        }

 

        /// <summary>
        /// Retrieves a list of all authors asynchronously.
        /// </summary>
        public static async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            IEnumerable<Author> authors = await _authorrepo.GetAllAsync();
            return authors;
        }

        /// <summary>
        /// Retrieves a list of all AuthorViewModel asynchronously.
        /// </summary>
        public static async Task<IEnumerable<AuthorViewModel>> GetAuthorViewModelAsync()
        {
            IEnumerable<Author> authors = await _authorrepo.GetAllAsync();
            return authors.Select(author => new AuthorViewModel { Id = author.Id, Name = author.FullName });
        }



        /// <summary>
        /// Finds an author by ID asynchronously.
        /// </summary>
        public static async Task<Author?> FindAsync(int Id)
        {
            Author? author = await _authorrepo.GetByIdAsync(Id);
            return author;
        }

        /// <summary>
        /// Checks if an author exists by ID.
        /// </summary>
        public static async Task<bool> IsExists(int Id)
        {
            return await _authorrepo.IsExistsAsync(Id);
        }

        /// <summary>
        /// Adds a new author asynchronously.
        /// </summary>
        public async Task<bool> _AddAsync(Author author)
        {
            // Validation logic...

            Author? newAuthor = await _authorrepo.InsertAsync(author);

            if (newAuthor != null) 
                author.Id = newAuthor.Id;
            return (newAuthor == null)? false : newAuthor.Id > 0;
        }

        /// <summary>
        /// Updates an existing author asynchronously.
        /// </summary>
        public Task<bool> _UpdateAsync(Author author)
        {
            // Validation logic...
            return _authorrepo.UpdateAsync(author);
        }

        /// <summary>
        /// Deletes an author asynchronously by ID.
        /// </summary>
        public static async Task<bool> DeleteAsync(int Id)
        {
            return await _authorrepo.Delete(Id);
        }

    }
}
