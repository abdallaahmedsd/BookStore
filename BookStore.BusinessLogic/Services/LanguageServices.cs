using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.Services
{
    /// <summary>
    /// Provides services for managing languages.
    /// </summary>
    public class LanguageServices
    {
        /// <summary>
        /// Represents the mode of operation (Add or Update).
        /// </summary>
        public enum enMode { Add = 0, Update = 1 }

        /// <summary>
        /// Current mode of the service (Add or Update).
        /// </summary>
        private enMode Mode = enMode.Add;

        private static readonly string _connectionString = "Server=.;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;";
        private static readonly LanguageRepository _languageRepository;

        /// <summary>
        /// Gets the unique identifier for the language.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the name of the language.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initializes the <see cref="LanguageServices"/> class.
        /// </summary>
        static LanguageServices()
        {
            _languageRepository = new LanguageRepository(_connectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageServices"/> class for adding a new language.
        /// </summary>
        /// <param name="language">The language entity.</param>
        public LanguageServices()
        {
            Mode = enMode.Add;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageServices"/> class for updating an existing language.
        /// </summary>
        /// <param name="language">The language entity.</param>
        /// <param name="mode">The mode (default is Update).</param>
        private LanguageServices(Language language, enMode mode = enMode.Update)
        {
            Id = language.Id;
            Name = language.LanguageName;
            Mode = mode;
        }

        /// <summary>
        /// Finds a language by its ID asynchronously.
        /// </summary>
        /// <param name="id">The language ID.</param>
        /// <returns>A <see cref="LanguageServices"/> instance if found; otherwise, null.</returns>
        public static async Task<LanguageServices?> FindAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            Language? language = await _languageRepository.GetByIdAsync(id);
            return language == null ? null : new LanguageServices(language, enMode.Update);
        }

        /// <summary>
        /// Finds a language by its name asynchronously.
        /// </summary>
        /// <param name="languageName">The language name.</param>
        /// <returns>A <see cref="LanguageServices"/> instance if found; otherwise, null.</returns>
        public static async Task<LanguageServices?> FindAsync(string languageName)
        {
            if (string.IsNullOrWhiteSpace(languageName))
            {
                return null;
            }
            Language? language = await _languageRepository.GetByNameAsync(languageName);
            return language == null ? null : new LanguageServices(language, enMode.Update);
        }

        /// <summary>
        /// Retrieves all languages asynchronously.
        /// </summary>
        /// <returns>A collection of <see cref="Language"/> entities.</returns>
        public static async Task<IEnumerable<Language>> GetAllAsync()
        {
            IEnumerable<Language> languages = await _languageRepository.GetAllAsync();
            return languages;
        }
    }
}
