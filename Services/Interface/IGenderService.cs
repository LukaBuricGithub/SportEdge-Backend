using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;

namespace SportEdge.API.Services.Interface
{
    /// <summary>
    /// Defines service operations for managing genders.
    /// </summary>
    public interface IGenderService
    {

        /// <summary>
        /// Creates a new gender.
        /// </summary>
        /// <param name="request">DTO containing data for the new gender.</param>
        /// <returns>The created gender as a DTO.</returns>
        Task<GenderDto> CreateAsync(CreateGenderRequestDto request);


        /// <summary>
        /// Retrieves all genders.
        /// </summary>
        /// <returns>A list of gender DTOs.</returns>
        Task<List<GenderDto>> GetAllAsync();


        /// <summary>
        /// Retrieves a gender by its ID.
        /// </summary>
        /// <param name="id">The gender ID.</param>
        /// <returns>The gender DTO.</returns>
        Task<GenderDto> GetAsync(int id);


        /// <summary>
        /// Updates a gender with new data.
        /// </summary>
        /// <param name="id">The ID of the gender to update.</param>
        /// <param name="request">DTO containing updated data.</param>
        /// <returns>The updated gender DTO.</returns>
        Task<GenderDto> UpdateAsync(int id, UpdateGenderRequestDto request);


        /// <summary>
        /// Deletes a gender by its ID.
        /// </summary>
        /// <param name="id">The ID of the gender to delete.</param>
        /// <returns>True if the gender was deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);

    }
}
