using Microsoft.AspNetCore.Mvc;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.Enums;

namespace SportEdge.API.Repositories.Interface
{
    /// <summary>
    /// Interface for category-related data operations.
    /// </summary>
    public interface ICategoryRepository
    {

        /// <summary>
        /// Adds a new category to the database.
        /// </summary>
        /// <param name="category">The category entity to create.</param>
        /// <returns>The created category entity.</returns>
        Task<Category> CreateAsync(Category category);


        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category entity if found; otherwise, null.</returns>
        Task<Category> GetAsync(int id);



        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of all category entities.</returns>
        Task<List<Category>> GetAllAsync();



        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="category">The category entity with updated data.</param>
        /// <returns>The updated category entity.</returns>
        Task<Category> UpdateAsync(Category category);


        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>Delete result enum which contains information if entity is deleted.</returns>
        Task<DeleteResult> DeleteAsync(int id);



        /// <summary>
        /// Retrieves all categories whose IDs are inside a search list.
        /// </summary>
        /// <param name="ids">The list of category IDs to search from.</param>
        /// <returns>A list of all matching category entities.</returns>
        Task<List<Category>> GetByIdsAsync(IEnumerable<int> ids);
    }
}
