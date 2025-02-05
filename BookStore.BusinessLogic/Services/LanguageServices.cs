using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Utilties.BusinessHelpers;
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
        private readonly LanguageRepository _languageRepository;

        public LanguageServices()
        {
            _languageRepository = new LanguageRepository(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Finds a language by ID asynchronously.
        /// </summary>
        /// <param name="id">The language ID.</param>
        /// <returns>The <see cref="Language"/> entity if found; otherwise, null.</returns>
        public async Task<Language?> FindAsync(int id)
        {
            if (id <= 0)
                return null;

            return await _languageRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Finds a language by name asynchronously.
        /// </summary>
        /// <param name="languageName">The language name.</param>
        /// <returns>The <see cref="Language"/> entity if found; otherwise, null.</returns>
        public async Task<Language?> FindAsync(string languageName)
        {
            if (string.IsNullOrWhiteSpace(languageName))
                return null;

            return await _languageRepository.GetByNameAsync(languageName.Trim());
        }

        /// <summary>
        /// Deletes a language by ID asynchronously.
        /// </summary>
        /// <param name="id">The language ID.</param>
        /// <returns>True if the language was deleted; otherwise, false.</returns>
        //public async Task<bool> DeleteAsync(int id)
        //{
        //    if (id <= 0)
        //        return false;

        //    return await _languageRepository.Delete(id);
        //}

        /// <summary>
        /// Gets all languages asynchronously.
        /// </summary>
        /// <returns>A collection of <see cref="Language"/> entities.</returns>
        public async Task<IEnumerable<Language>> GetAllAsync()
        {
            return await _languageRepository.GetAllAsync();
        }

        /// <summary>
        /// Adds a new language asynchronously.
        /// </summary>
        /// <param name="newLanguage">The new language to add.</param>
        /// <returns>True if the language was added successfully; otherwise, false.</returns>
        //public async Task<bool> AddAsync(Language newLanguage)
        //{
        //    if (newLanguage == null)
        //        throw new ArgumentNullException(nameof(newLanguage));

        //    Language? language = await _languageRepository.InsertAsync(newLanguage);
        //    newLanguage.Id = language?.Id ?? 0;
        //    return newLanguage.Id > 0;
        //}

        /// <summary>
        /// Updates an existing language asynchronously.
        /// </summary>
        /// <param name="language">The language to update.</param>
        /// <returns>True if the language was updated successfully; otherwise, false.</returns>
        //public async Task<bool> UpdateAsync(Language language)
        //{
        //    if (language == null)
        //        throw new ArgumentNullException(nameof(language));

        //    return await _languageRepository.UpdateAsync(language);
        //}

        /// <summary>
        /// Checks if a language exists by ID asynchronously.
        /// </summary>
        /// <param name="id">The language ID.</param>
        /// <returns>True if the language exists; otherwise, false.</returns>
        public async Task<bool> IsExistAsync(int id)
        {
            if (id <= 0)
                return false;

            return await _languageRepository.IsExistsAsync(id);
        }
    }
}
