using SportEdge.API.Models.Domain;

namespace SportEdge.API.Repositories.Interface
{
    /// <summary>
    /// Interface for size option-related data operations.
    /// </summary>
    public interface ISizeOptionRepository
    {

        /// <summary>
        /// Adds a new size option to the database.
        /// </summary>
        /// <param name="sizeOption">The size option entity to create.</param>
        /// <returns>The created size option entity.</returns>
        Task<SizeOption> CreateAsync(SizeOption sizeOption);


        /// <summary>
        /// Retrieves all size options.
        /// </summary>
        /// <returns>A list of all size option entities.</returns>
        Task<List<SizeOption>> GetAllAsync();


        /// <summary>
        /// Retrieves all size options for specified gender.
        /// </summary>
        /// <param name="genderId">The ID of gender.</param>
        /// <returns>A list of size option entities for desired gender.</returns>
        Task<List<SizeOption>> GetSizeOptionsByGenderIdAsync(int genderId);



        /// <summary>
        /// Retrieves a size option by its ID.
        /// </summary>
        /// <param name="id">The ID of the size option to retrieve.</param>
        /// <returns>The size option entity if found; otherwise, null.</returns>
        Task<SizeOption> GetAsync(int id);



        /// <summary>
        /// Updates an existing size option.
        /// </summary>
        /// <param name="sizeOption">The size option entity with updated data.</param>
        /// <returns>The updated size option entity.</returns>
        Task<SizeOption> UpdateAsync(SizeOption sizeOption);


        /// <summary>
        /// Deletes a size option by ID.
        /// </summary>
        /// <param name="id">The ID of the size option to delete.</param>
        /// <returns>True if deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
