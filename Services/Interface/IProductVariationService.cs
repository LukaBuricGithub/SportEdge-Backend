using SportEdge.API.Models.DTO;

namespace SportEdge.API.Services.Interface
{
    /// <summary>
    /// Defines service operations for managing product variations.
    /// </summary>
    public interface IProductVariationService
    {

        /// <summary>
        /// Creates a new product variation.
        /// </summary>
        /// <param name="request">DTO containing data for the new product variation.</param>
        /// <returns>The created product variation as a DTO.</returns>
        Task<ProductVariationDto> CreateAsync(CreateProductVariationRequestDto request);


        /// <summary>
        /// Retrieves all product variations.
        /// </summary>
        /// <returns>A list of product variation DTOs.</returns>
        Task<List<ProductVariationDto>> GetAllAsync();


        /// <summary>
        /// Retrieves a product variation by its ID.
        /// </summary>
        /// <param name="id">The product variation ID.</param>
        /// <returns>The product variation DTO.</returns>
        Task<ProductVariationDto> GetAsync(int id);


        /// <summary>
        /// Updates a product variation with new data.
        /// </summary>
        /// <param name="id">The ID of the product variation to update.</param>
        /// <param name="request">DTO containing updated data.</param>
        /// <returns>The updated product variation DTO.</returns>
        Task<ProductVariationDto> UpdateAsync(int id, UpdateProductVariationRequestDto request);




        /// <summary>
        /// Updates multiple product variations with new data.
        /// </summary>
        /// <param name="request">DTO containing updated data.</param>
        /// <returns>The list of updated product variations in DTO.</returns>
        Task<List<ProductVariationDto>> UpdateMultipleProductVariationsAsync(UpdateMultipleProductVariationsRequestDto request);


        /// <summary>
        /// Retrieves all product variations that associated with a specific product ID.
        /// </summary>
        /// <param name="productId">Product ID.</param>
        /// <returns>A list of matching product variation DTOs.</returns>
        Task<List<ProductVariationDto>> GetAllForProduct(int productId);
    }
}
