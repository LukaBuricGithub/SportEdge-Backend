using SportEdge.API.Models.DTO;

namespace SportEdge.API.Services.Interface
{
    /// <summary>
    /// Defines service operations for managing products.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="request">DTO containing data for the new product.</param>
        /// <returns>The created product as a DTO.</returns>
        Task<ProductDto> CreateAsync(CreateProductRequestDto request);


        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of product DTOs.</returns>
        Task<List<ProductDto>> GetAllAsync();


        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>The product DTO.</returns>
        Task<ProductDto> GetAsync(int id);


        /// <summary>
        /// Updates a product with new data.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="request">DTO containing updated data.</param>
        /// <returns>The updated product DTO.</returns>
        Task<ProductDto> UpdateAsync(int id, UpdateProductRequestDto request);


        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>True if the product was deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);



        /// <summary>
        /// Retrieves all products that match query.
        /// </summary>
        /// <param name="query">The query by which products are searched.</param>
        /// <returns>A list of matching product DTOs.</returns>
        Task<List<ProductDto>> SearchProductsAsync(string? query);

    }
}
