using BookStore.BusinessLogic.Interfaces;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Entities;
using BookStore.Utilties.BusinessHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.Services
{
    /// <summary>
    /// Provides services for managing categories.
    /// </summary>
    public class CategoryServices: IServices
    {
        /// <summary>
        /// Data Transfer Object for the current category.
        /// </summary>
        //public CategoryDTO CDTO => new CategoryDTO(Id, Name, CreatedBy);

        /// <summary>
        /// Enumeration for mode (Add or Update).
        /// </summary>
        public enum enMode { Add = 0, Update = 1 }

        /// <summary>
        /// Current mode of the service (Add or Update).
        /// </summary>
        private enMode Mode = enMode.Add;

        private static readonly CategoryRepository _categoryRepository;

        /// <summary>
        /// Category ID.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Category name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// ID of the user who created the category.
        /// </summary>
        public int CreatedBy { get; private set; }

        /// <summary>
        /// Static constructor to initialize the category repository.
        /// </summary>
        static CategoryServices()
        {
            _categoryRepository = new CategoryRepository(ConnectionConfig._connectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryServices"/> class with the specified category and mode.
        /// </summary>
        /// <param name="categoryDTO">The category data transfer object.</param>
        /// <param name="mode">The mode (Add or Update).</param>
        public CategoryServices()
        {
            Name = "";
            Mode = enMode.Add;
        }

        private CategoryServices(Category category, enMode mode = enMode.Update)
        {
            Id = category.Id;
            Name = category.Name;
            CreatedBy = category.CreatedBy;
            Mode = enMode.Update;
        }


        /// <summary>
        /// Gets the category name asynchronously.
        /// </summary>
        /// <param name="categoryId">The category ID.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the category name.</returns>
        public static async Task<string> GetCategoryNameAsync(int categoryId)
        {
            Category? category = await _categoryRepository.GetByIdAsync(categoryId);
            return category?.Name ?? "";
        }

        /// <summary>
        /// Maps a <see cref="Category"/> entity to a <see cref="CategoryDTO"/>.
        /// </summary>
        /// <param name="category">The category entity.</param>
        /// <returns>A <see cref="CategoryDTO"/> object.</returns>
        //private static CategoryDTO _MapCategoryDTO(Category category)
        //{
        //    return new CategoryDTO(category.Id, category.Name, category.CreatedBy);
        //}

        /// <summary>
        /// Finds a category by ID asynchronously.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the <see cref="CategoryServices"/> instance.</returns>
        public static async Task<CategoryServices?> FindAsync(int id)
        {
            if (id <= 0)
                return null;
            Category? category = await _categoryRepository.GetByIdAsync(id);
            return category == null ? null : new CategoryServices(category, enMode.Update);
        }

        /// <summary>
        /// Finds a category by name asynchronously.
        /// </summary>
        /// <param name="categoryName">The category name.</param>
        /// <returns>A task representing the asynchronous operation. The task result is the <see cref="CategoryServices"/> instance.</returns>
        public static async Task<CategoryServices?> FindAsync(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return null;

            Category? category = await _categoryRepository.GetByNameAsync(categoryName.Trim());
            return category == null ? null : new CategoryServices(category, enMode.Update);
        }

        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="Id">The category ID.</param>
        /// <returns>True if the category is deleted successfully; otherwise, false.</returns>
        public static async Task<bool> Delete(int Id)
        {
            if (Id <= 0)
                return false;
            return await _categoryRepository.Delete(Id);
        }

        /// <summary>
        /// Gets all categories asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is an enumerable collection of <see cref="CategoryDTO"/> objects.</returns>
        public static async Task<IEnumerable<Category>> GetAllAsync()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllAsync();
            return categories;
        }

        /// <summary>
        /// Adds a new category asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is true if the category is added successfully; otherwise, false.</returns>
        private async Task<bool> _AddAsync()
        {
            Category? category = await _categoryRepository.InsertAsync(new Category { Name = Name, CreatedBy = CreatedBy });
            this.Id = category?.Id ?? 0;
            return this.Id > 0;
        }

        /// <summary>
        /// Updates an existing category asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is true if the category is updated successfully; otherwise, false.</returns>
        private async Task<bool> _UpdateAsync()
        {
            return await _categoryRepository.UpdateAsync(new Category { Id = Id, Name = Name });
        }

        /// <summary>
        /// Saves the current category asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result is true if the category is saved successfully; otherwise, false.</returns>
        public async Task<bool> SaveAsync()
        {
            switch (Mode)
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
