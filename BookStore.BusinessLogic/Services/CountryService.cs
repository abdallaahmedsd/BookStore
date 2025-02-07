using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.Entities;
using BookStore.DataAccess.Repositories;
using BookStore.Utilties.BusinessHelpers;
using Azure.Core;

namespace BookstoreBackend.BLL.Services
{
    /// <summary>
    /// Provides services related to country operations.
    /// </summary>
    public class CountryService
    {

        private static readonly CountryRepository _countryRepository;
       
        /// <summary>
        /// Initializes the static members of the <see cref="CountryService"/> class.
        /// </summary>
        static CountryService()
        {
            _countryRepository = new CountryRepository(ConnectionConfig._connectionString);
        }


        /// <summary>
        /// Retrieves all countries asynchronously.
        /// </summary>
        /// <returns>A collection of <see cref="Country"/> instances.</returns>
        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            IEnumerable<Country> countries = await _countryRepository.GetAllAsync();
            return countries;
        }

        /// <summary>
        /// Finds a country by its ID asynchronously.
        /// </summary>
        /// <param name="id">The country ID.</param>
        /// <returns>A <see cref="Country"/> instance if found; otherwise, null.</returns>
        public async Task<Country?> FindAsync(int id)
        {
            Country? country = await _countryRepository.GetByIdAsync(id);
            return country;
        }

        /// <summary>
        /// Finds a country by its name asynchronously.
        /// </summary>
        /// <param name="name">The country name.</param>
        /// <returns>A <see cref="Country"/> instance if found; otherwise, null.</returns>
        public async Task<Country?> FindAsync(string name)
        {
            Country? country = await _countryRepository.GetCountryByNameAsync(name);
            return country;
        }
    }
}
