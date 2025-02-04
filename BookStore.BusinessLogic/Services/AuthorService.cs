using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;

namespace BookstoreBackend.BLL.Services
{
    public class AuthorService
    {

        public enum enMode { Add = 0, Update = 1 }

     
        private enMode Mode = enMode.Add;

     
        private static readonly string _connectionString = "Server=.;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;";

        private static readonly AuthorRepository _authorrepo;
        public int Id { get; set; }
        public string? Bio { get; set; }
        public int CreatedBy { get; set; }
        public string FullName { get { return FirstName + ' ' + LastName; } private set { } }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NationalityID { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public string? ProfileImage { get; set; }

        public async Task<CountryService?> GetCountryInfo()
        {
            return await CountryService.FindAsync(this.NationalityID);
        }
        static AuthorService()
        {
            _authorrepo = new AuthorRepository(_connectionString);
        }
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
            Country = CountryService.FindAsync(NationalityID);

            this.Mode = enMode.Update;
        }

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

        public static async Task<IEnumerable<AuthorService>> GetAuthorsAsync()
        {
            IEnumerable<Author> authors = await _authorrepo.GetAllAsync();
            return authors.Select(author => new AuthorService(author)).ToList();
        }

        public static async Task<AuthorService?> FindAsync(int Id)
        {
            Author? author = await _authorrepo.GetByIdAsync(Id);
            return (author == null) ? null : new AuthorService(author);
        }

        public static async Task<bool> IsExists(int Id)
        {
            return await _authorrepo.IsExistsAsync(Id);
        }

        private async Task<bool> _AddAsync()
        {
            if (string.IsNullOrWhiteSpace(this.FirstName))
                throw new ArgumentException("First Name is required.");

            if (string.IsNullOrWhiteSpace(this.LastName))
                throw new ArgumentException("Last Name is required.");

            if (string.IsNullOrWhiteSpace(this.Email))
                throw new ArgumentException("Email is required.");

            if (!Regex.IsMatch(this.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Invalid email format.");

            if (this.NationalityID <= 0)
                throw new ArgumentException("Invalid Nationality ID.");

            if (this.CreatedBy <= 0)
                throw new ArgumentException("Invalid User ID.");

            if (this.Phone != null && !string.IsNullOrWhiteSpace(this.Phone) && this.Phone.Length > 15 || this.Phone.Length > 11)
                throw new ArgumentException("Phone number is invalid.");

            if (this.Bio != null && !string.IsNullOrWhiteSpace(this.Bio) && this.Bio.Length > 500)
                throw new ArgumentException("Bio is too long (max 500 characters).");


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
            
            this.Id = (author == null || author.Id <= 0)? -1 : author.Id;   
            return author.Id > 0;

        }

        private Task<bool> _UpdateAsync() {

            if (this.FirstName != null && string.IsNullOrWhiteSpace(this.FirstName))
                throw new ArgumentException("First Name is required.");

            if (this.LastName != null && string.IsNullOrWhiteSpace(this.LastName))
                throw new ArgumentException("Last Name is required.");

            if (this.Email != null && string.IsNullOrWhiteSpace(this.Email))
                throw new ArgumentException("Email is required.");

            if (this.Email != null && !Regex.IsMatch(this.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Invalid email format.");

            if (this.NationalityID != null && this.NationalityID <= 0)
                throw new ArgumentException("Invalid Nationality ID.");

            if (this.CreatedBy != null && this.CreatedBy <= 0)
                throw new ArgumentException("Invalid User ID.");

            if (this.Phone != null && !string.IsNullOrWhiteSpace(this.Phone) && this.Phone.Length > 15 || this.Phone.Length > 11)
                throw new ArgumentException("Phone number is invalid.");

            if (this.Bio != null && !string.IsNullOrWhiteSpace(this.Bio) && this.Bio.Length > 500)
                throw new ArgumentException("Bio is too long (max 500 characters).");


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

        public async Task<bool> DeleteAsync()
        {
            return await DeleteAsync(this.Id);
        }

        public static async Task<bool> DeleteAsync(int Id)
        {
            return await _authorrepo.Delete(Id);
        }
        public async Task<bool> SaveAsync()
        {
            switch (this.Mode)
            {
                case enMode.Add:
                {
                    if (await _AddAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                }
                case enMode.Update:
                    return await _UpdateAsync();

                default:
                    return false;
            }
        }
    }
}
