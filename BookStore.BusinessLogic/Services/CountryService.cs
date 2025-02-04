using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.Entities;
using BookStore.DataAccess.Repositories;
using BookStore.Utilties.BusinessHelpers;

namespace BookstoreBackend.BLL.Services
{
    /// <summary>
    /// Provides services related to country operations.
    /// </summary>
    public class CountryService
    {
        /// <summary>
        /// Gets or sets the country ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the country name.
        /// </summary>
        public string Name { get; set; }

        private static readonly CountryRepository _countryRepository;
       
        /// <summary>
        /// Initializes the static members of the <see cref="CountryService"/> class.
        /// </summary>
        static CountryService()
        {
            _countryRepository = new CountryRepository(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryService"/> class using a <see cref="Country"/> entity.
        /// </summary>
        /// <param name="country">The country entity.</param>
        public CountryService(Country country)
        {
            Id = country.Id;
            Name = country.Name;
        }

        /// <summary>
        /// Retrieves all countries asynchronously.
        /// </summary>
        /// <returns>A collection of <see cref="CountryService"/> instances.</returns>
        public static async Task<IEnumerable<CountryService>> GetCountriesAsync()
        {
            IEnumerable<Country> countries = await _countryRepository.GetAllAsync();
            return countries.Select(country => new CountryService(country)).ToList();
        }

        /// <summary>
        /// Finds a country by its ID asynchronously.
        /// </summary>
        /// <param name="id">The country ID.</param>
        /// <returns>A <see cref="CountryService"/> instance if found; otherwise, null.</returns>
        public static async Task<CountryService?> FindAsync(int id)
        {
            Country? country = await _countryRepository.GetByIdAsync(id);
            return (country == null) ? null : new CountryService(country);
        }

        /// <summary>
        /// Finds a country by its name asynchronously.
        /// </summary>
        /// <param name="name">The country name.</param>
        /// <returns>A <see cref="CountryService"/> instance if found; otherwise, null.</returns>
        public static async Task<CountryService?> FindAsync(string name)
        {
            Country? country = await _countryRepository.GetCountryByNameAsync(name);
            return (country == null) ? null : new CountryService(country);
        }
    }
}
