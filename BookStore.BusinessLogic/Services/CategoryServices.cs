using BookStore.BusinessLogic.Interfaces;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Admin;
using BookStore.Utilties.BusinessHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.Services
{
    /// <summary>
    /// Provides services for managing categories.
    /// </summary>
    public class CategoryServices
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryServices()
        {
            _categoryRepository = new CategoryRepository(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Gets the category name asynchronously.
        /// </summary>
        /// <param name="categoryId">The category ID.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the category name.</returns>
        public async Task<string> GetCategoryNameAsync(int categoryId)
        {
            Category? category = await _categoryRepository.GetByIdAsync(categoryId);
            return category?.Name ?? "";
        }

        /// <summary>
        /// Finds a category by ID asynchronously.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the <see cref="Category"/> instance.</returns>
        public async Task<Category?> FindAsync(int id)
        {
            if (id <= 0)
                return null;
            return await _categoryRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Finds a category by name asynchronously.
        /// </summary>
        /// <param name="categoryName">The category name.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the <see cref="Category"/> instance.</returns>
        public async Task<Category?> FindAsync(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return null;

            return await _categoryRepository.GetByNameAsync(categoryName.Trim());
        }


        /// <summary>
        /// Retrieves a <see cref="CategoryViewModel"/> based on the provided category ID.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="CategoryViewModel"/> if found; otherwise, <c>null</c>.
        /// </returns>
        public async Task<CategoryViewModel?> GetCategoryViewModel(int id)
        {
            if (id <= 0)
                return null;

            Category? category = await FindAsync(id);

            if (category == null)
                return null;

            return new CategoryViewModel { Id = category.Id, Name = category.Name };
        }

        /// <summary>
        /// Retrieves a <see cref="CategoryViewModel"/> based on the provided category name.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="CategoryViewModel"/> if found; otherwise, <c>null</c>.
        /// </returns>
        public async Task<CategoryViewModel?> GetCategoryViewModel(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            Category? category = await FindAsync(name.Trim());

            if (category == null)
                return null;

            return new CategoryViewModel { Id = category.Id, Name = category.Name };
        }


        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>True if the category is deleted successfully; otherwise, false.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                return false;
            return await _categoryRepository.Delete(id);
        }

        /// <summary>
        /// Gets all categories asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is an enumerable collection of <see cref="Category"/> objects.</returns>
        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllAsync();
            return  categories.Select(c => new CategoryViewModel { Id = c.Id,Name=c.Name });
        }

        /// <summary>
        /// Adds a new category asynchronously.
        /// </summary>
        /// <param name="newCategory">The new category to add.</param>
        /// <returns>A task representing the asynchronous operation. The task result is true if the category is added successfully; otherwise, false.</returns>
        public async Task<bool> AddAsync(Category newCategory)
        {
            if (newCategory == null) return false;
            Category? category = await _categoryRepository.InsertAsync(newCategory);
            newCategory.Id = category?.Id ?? 0;
            return newCategory.Id > 0;
        }

        /// <summary>
        /// Updates an existing category asynchronously.
        /// </summary>
        /// <param name="category">The category to update.</param>
        /// <returns>A task representing the asynchronous operation. The task result is true if the category is updated successfully; otherwise, false.</returns>
        public async Task<bool> UpdateAsync(Category category)
        {
            if (category == null) return false;
            return await _categoryRepository.UpdateAsync(category);
        }

        /// <summary>
        /// Checks if a category exists by ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>A task representing the asynchronous operation. The task result is true if the category exists; otherwise, false.</returns>
        public async Task<bool> IsExistAsync(int id)
        {
            if (id <= 0) return false;
            return await _categoryRepository.IsExistsAsync(id);
        }
    }
}
