using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Utilties.BusinessHelpers;

namespace BookstoreBackend.BLL.Services
{
    /// <summary>
    /// Service class for managing authors in the bookstore system.
    /// </summary>
    public class AuthorService
    {
        private enum enMode { Add = 0, Update = 1 }

        private enMode Mode = enMode.Add;


        private static readonly AuthorRepository _authorrepo;

        /// <summary>
        /// Gets or sets the author ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the biography of the author.
        /// </summary>
        public string? Bio { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created the author entry.
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets the full name of the author.
        /// </summary>
        public string FullName { get { return FirstName + ' ' + LastName; } private set { } }

        /// <summary>
        /// Gets or sets the first name of the author.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the author.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the nationality ID of the author.
        /// </summary>
        public int NationalityID { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the author.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets the email of the author.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the profile image of the author.
        /// </summary>
        public string? ProfileImage { get; set; }

        /// <summary>
        /// Retrieves country information based on the author's nationality ID.
        /// </summary>
        public async Task<CountryService?> GetCountryInfo()
        {
            return await CountryService.FindAsync(this.NationalityID);
        }

        static AuthorService()
        {
            _authorrepo = new AuthorRepository(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorService"/> class using an existing author.
        /// </summary>
        public AuthorService(Author author)
        {
            Id = author.Id;
            Bio = author.Bio;
            CreatedBy = author.CreatedBy;
            FirstName = author.FirstName;
            LastName = author.LastName;
            NationalityID = author.NationalityID;
            Phone = author.Phone;
            Email = author.Email;
            ProfileImage = author.ProfileImage;

            this.Mode = enMode.Update;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorService"/> class for adding a new author.
        /// </summary>
        public AuthorService()
        {
            Id = -1;
            Bio = null;
            CreatedBy = -1;
            FirstName = string.Empty;
            LastName = string.Empty;
            NationalityID = -1;
            Phone = null;
            Email = string.Empty;
            ProfileImage = null;

            this.Mode = enMode.Add;
        }

        /// <summary>
        /// Retrieves a list of all authors asynchronously.
        /// </summary>
        public static async Task<IEnumerable<AuthorService>> GetAuthorsAsync()
        {
            IEnumerable<Author> authors = await _authorrepo.GetAllAsync();
            return authors.Select(author => new AuthorService(author)).ToList();
        }

        /// <summary>
        /// Finds an author by ID asynchronously.
        /// </summary>
        public static async Task<AuthorService?> FindAsync(int Id)
        {
            Author? author = await _authorrepo.GetByIdAsync(Id);
            return (author == null) ? null : new AuthorService(author);
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
        private async Task<bool> _AddAsync()
        {
            // Validation logic...

            Author? author = await _authorrepo.InsertAsync(new Author
            {
                Bio = this.Bio,
                CreatedBy = this.CreatedBy,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                ProfileImage = this.ProfileImage,
                NationalityID = this.NationalityID,
                Phone = this.Phone
            });

            this.Id = (author == null || author.Id <= 0) ? -1 : author.Id;
            return this.Id > 0;
        }

        /// <summary>
        /// Updates an existing author asynchronously.
        /// </summary>
        private Task<bool> _UpdateAsync()
        {
            // Validation logic...
            return _authorrepo.UpdateAsync(new Author
            {
                Id = this.Id,
                Bio = this.Bio,
                CreatedBy = this.CreatedBy,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                ProfileImage = this.ProfileImage,
                NationalityID = this.NationalityID,
                Phone = this.Phone
            });
        }

        /// <summary>
        /// Deletes an author asynchronously by ID.
        /// </summary>
        public static async Task<bool> DeleteAsync(int Id)
        {
            return await _authorrepo.Delete(Id);
        }

        /// <summary>
        /// Saves the current author (either adding or updating) asynchronously.
        /// </summary>
        public async Task<bool> SaveAsync()
        {
            switch (this.Mode)
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
