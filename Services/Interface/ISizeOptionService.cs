using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;

namespace SportEdge.API.Services.Interface
{
    /// <summary>
    /// Defines service operations for managing size options.
    /// </summary>
    public interface ISizeOptionService
    {
        /// <summary>
        /// Creates a new size option.
        /// </summary>
        /// <param name="request">DTO containing data for the new size option.</param>
        /// <returns>The created size option as a DTO.</returns>
        Task<SizeOptionDto> CreateAsync(CreateSizeOptionRequestDto request);



        /// <summary>
        /// Retrieves all size options.
        /// </summary>
        /// <returns>A list of size option DTOs.</returns>
        Task<List<SizeOptionDto>> GetAllAsync();



        /// <summary>
        /// Retrieves a size option by its ID.
        /// </summary>
        /// <param name="id">The size option ID.</param>
        /// <returns>The size option DTO.</returns>
        Task<SizeOptionDto> GetAsync(int id);


        /// <summary>
        /// Updates a size option with new data.
        /// </summary>
        /// <param name="id">The ID of the size option to update.</param>
        /// <param name="request">DTO containing updated data.</param>
        /// <returns>The updated size option DTO.</returns>
        Task<SizeOptionDto> UpdateAsync(int id, UpdateSizeOptionRequestDto request);



        /// <summary>
        /// Deletes a size option by its ID.
        /// </summary>
        /// <param name="id">The ID of the size option to delete.</param>
        /// <returns>True if the size option was deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);


        /// <summary>
        /// Retrieves all size options for specified gender.
        /// </summary>
        /// <param name="genderId">The ID of gender.</param>
        /// <returns>A list of size option DTOs for desired gender.</returns>
        Task<List<SizeOptionDto>> GetSizeOptionsByGenderIdAsync(int genderId); 
    }
}
