using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;

namespace SportEdge.API.Services.Interface
{
    /// <summary>
    /// Defines service operations for managing brands.
    /// </summary>
    public interface IBrandService
    {
        /// <summary>
        /// Creates a new brand.
        /// </summary>
        /// <param name="request">DTO containing data for the new brand.</param>
        /// <returns>The created brand as a DTO.</returns>
        Task<BrandDto> CreateAsync(CreateBrandRequestDto request);


        /// <summary>
        /// Retrieves all brands.
        /// </summary>
        /// <returns>A list of brand DTOs.</returns>
        Task<List<BrandDto>> GetAllAsync();


        /// <summary>
        /// Retrieves a brand by its ID.
        /// </summary>
        /// <param name="id">The brand ID.</param>
        /// <returns>The brand DTO.</returns>
        Task<BrandDto> GetAsync(int id);


        /// <summary>
        /// Updates a brand with new data.
        /// </summary>
        /// <param name="id">The ID of the brand to update.</param>
        /// <param name="request">DTO containing updated data.</param>
        /// <returns>The updated brand DTO.</returns>
        Task<BrandDto> UpdateAsync(int id, UpdateBrandRequestDto request);


        /// <summary>
        /// Deletes a brand by its ID.
        /// </summary>
        /// <param name="id">The ID of the brand to delete.</param>
        /// <returns>True if the brand was deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);


        /// <summary>
        /// Searches for brands by a partial or full name.
        /// </summary>
        /// <param name="search">The search string.</param>
        /// <returns>A list of matching brand DTOs.</returns>
        Task<List<BrandDto>> GetBrandsByNameAsync(string search);
    }
}
