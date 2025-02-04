using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreBackend.DAL.Configurations;
using RepoSP.Net.Services;
using BookStore.Models.Entities;
using BookStore.DataAccess.Configurations;

namespace BookStore.DataAccess.Repositories
{
    public class AuthorRepository : StoredProcedureRepository<Author>
    {

        private readonly string _connectionString;
        private readonly AuthorsStoredProcConfiguration _config;
        public AuthorRepository(string connectionString) : base(connectionString, AuthorsStoredProcConfiguration.Instance)
        {
            _connectionString = connectionString;
            _config = AuthorsStoredProcConfiguration.Instance;
        }
    }
}
