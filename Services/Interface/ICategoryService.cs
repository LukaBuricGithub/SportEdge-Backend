using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;

namespace SportEdge.API.Services.Interface
{
    /// <summary>
    /// Defines service operations for managing categories.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="request">DTO containing data for the new category.</param>
        /// <returns>The created category as a DTO.</returns>
        Task<CategoryDto> CreateAsync(CreateCategoryRequestDto request);

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of category DTOs.</returns>
        Task<List<CategoryDto>> GetAllAsync();


        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>The category DTO.</returns>
        Task<CategoryDto> GetAsync(int id);



        /// <summary>
        /// Updates a category with new data.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="request">DTO containing updated data.</param>
        /// <returns>The updated category DTO.</returns>
        Task<CategoryDto> UpdateAsync(int id, UpdateCategoryRequestDto request);


        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>True if the category was deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);



        /// <summary>
        /// Retrieves all root categories.
        /// </summary>
        /// <returns>Retrieves all root categories.</returns>
        Task<List<CategoryDto>> GetRootCategoriesAsync();
    }
}
