using SportEdge.API.Models.Domain;
using SportEdge.API.Models.Enums;

namespace SportEdge.API.Repositories.Interface
{
    /// <summary>
    /// Interface for brand-related data operations.
    /// </summary>
    public interface IBrandRepository
    {

        /// <summary>
        /// Adds a new brand to the database.
        /// </summary>
        /// <param name="brand">The brand entity to create.</param>
        /// <returns>The created brand entity.</returns>
        Task<Brand> CreateAsync(Brand brand);


        /// <summary>
        /// Retrieves a brand by its ID.
        /// </summary>
        /// <param name="id">The ID of the brand to retrieve.</param>
        /// <returns>The brand entity if found; otherwise, null.</returns>
        Task<Brand> GetAsync(int id);


        /// <summary>
        /// Retrieves all brands.
        /// </summary>
        /// <returns>A list of all brand entities.</returns>
        Task<List<Brand>> GetAllAsync();


        /// <summary>
        /// Searches for brands by name (partial or full match).
        /// </summary>
        /// <param name="search">The search term.</param>
        /// <returns>A list of matching brand entities.</returns>
        Task<List<Brand>> GetBrandsByNameAsync(string search);


        /// <summary>
        /// Updates an existing brand.
        /// </summary>
        /// <param name="brand">The brand entity with updated data.</param>
        /// <returns>The updated brand entity.</returns>
        Task<Brand> UpdateAsync(Brand brand);


        /// <summary>
        /// Deletes a brand by ID.
        /// </summary>
        /// <param name="id">The ID of the brand to delete.</param>
        /// <returns>Delete result enum which contains information if entity is deleted.</returns>
        Task<DeleteResult> DeleteAsync(int id);
    }
}
