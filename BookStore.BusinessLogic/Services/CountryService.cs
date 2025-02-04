using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Entities;
using BookStore.DataAccess.Repositories;
using System.Collections.Specialized;

namespace BookstoreBackend.BLL.Services
{
    public class CountryService
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private static readonly CountryRepository _countryRepository;
        private static readonly string _connectionString = "Server=.;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;";

        static CountryService()
        {
            _countryRepository = new CountryRepository(_connectionString);
        }

        public CountryService(Country country)
        {
            Id = country.Id;
            Name = country.Name;

        }


        public async static Task<IEnumerable<CountryService>> GetCountriesAsync()
        {
            IEnumerable<Country> countries = await _countryRepository.GetAllAsync();
            return countries.Select(country => new CountryService(country)).ToList();
        }

        public static async Task<CountryService?> FindAsync(int id)
        {
            Country? country = await _countryRepository.GetByIdAsync(id);
            return (country == null) ? null : new CountryService(country);
        }


        public static async Task<CountryService?> FindAsync(string Name)
        {
            Country? country = await _countryRepository.GetCountryByNameAsync(Name);
            return (country == null) ? null : new CountryService(country);
        }
    }
}
