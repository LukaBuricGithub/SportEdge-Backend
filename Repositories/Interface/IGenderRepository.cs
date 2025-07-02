using SportEdge.API.Models.Domain;

namespace SportEdge.API.Repositories.Interface
{
    /// <summary>
    /// Interface for gender-related data operations.
    /// </summary>
    public interface IGenderRepository
    {

        /// <summary>
        /// Adds a new gender to the database.
        /// </summary>
        /// <param name="gender">The gender entity to create.</param>
        /// <returns>The created gender entity.</returns>
        Task<Gender> CreateAsync(Gender gender);


        /// <summary>
        /// Retrieves a gender by its ID.
        /// </summary>
        /// <param name="id">The ID of the gender to retrieve.</param>
        /// <returns>The gender entity if found; otherwise, null.</returns>
        Task<Gender> GetAsync(int id);


        /// <summary>
        /// Retrieves all genders.
        /// </summary>
        /// <returns>A list of all gender entities.</returns>
        Task<List<Gender>> GetAllAsync();


        /// <summary>
        /// Updates an existing gender.
        /// </summary>
        /// <param name="gender">The gender entity with updated data.</param>
        /// <returns>The updated gender entity.</returns>
        Task<Gender> UpdateAsync(Gender gender);


        /// <summary>
        /// Deletes a gender by ID.
        /// </summary>
        /// <param name="id">The ID of the gender to delete.</param>
        /// <returns>True if deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);

    }
}
