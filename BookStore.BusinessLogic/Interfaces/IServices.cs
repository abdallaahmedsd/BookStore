using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.Interfaces
{
    /// <summary>
    /// Defines the contract for service classes in the BookstoreBackend.
    /// </summary>
    public interface IServices
    {
        /// <summary>
        /// Saves the entity asynchronously.
        /// </summary>
        /// <returns>True if the entity was saved; otherwise, false.</returns>
        Task<bool> SaveAsync();
    }
}
