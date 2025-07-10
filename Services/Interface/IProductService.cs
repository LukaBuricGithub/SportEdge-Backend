using SportEdge.API.Models.Domain;
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
        /// <param name="filter">Filter object that has pagination options.</param>
        /// <returns>A list of matching product DTOs.</returns>
        Task<FilteredProductsResultDto> SearchProductsAsync(string? query, ProductFilterDto filter);




        /// <summary>
        /// Retrieves all products that have a gender type as in entry query.
        /// </summary>
        /// <param name="name">The name of gender type you are looking for.</param>
        /// <param name="filter">Filter object that has pagination options.</param>
        /// <returns>A list of matching product DTOs.</returns>
        Task<FilteredProductsResultDto> GetProductsByGenderTypeAsync(string name, ProductFilterDto filter);



        /// <summary>
        /// Retrieves all products that match the given filter object.
        /// </summary>
        /// <param name="filter">The filter object used for searching products.</param>
        /// <returns>The list of matching products.</returns>
        //Task<List<ProductDto>> FilterProductsAsync(ProductFilterDto filter);
        Task<FilteredProductsResultDto> FilterProductsAsync(ProductFilterDto filter);


        /// <summary>
        /// Retrieves all products that have a categoryId as in the entry parameter.
        /// </summary>
        /// <param name="categoryId">The categoryId you wnt products to have.</param>
        /// <param name="filterDto">The filter object used for searching products.</param>
        /// <returns>A list of matching product DTOs.</returns>
        //Task<List<ProductDto>> GetProductsByCategoryIdAsync(int categoryId);
        Task<FilteredProductsResultDto> GetProductsByCategoryIdAsync(int categoryId, ProductFilterDto filterDto);


        /// <summary>
        /// Retrieves all products that match the given filter object.
        /// </summary>
        /// <param name="filter">The filter object used for searching products.</param>
        /// <returns>The list of matching products.</returns>
        Task<FilteredProductsResultDto> GetFilteredProductsAsync(ProductFilterWithTextDto filter);


    }
}
