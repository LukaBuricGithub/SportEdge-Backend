using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;

namespace SportEdge.API.Repositories.Interface
{
    /// <summary>
    /// Interface for product-related data operations.
    /// </summary>
    public interface IProductRepository
    {

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="product">The product entity to create.</param>
        /// <returns>The created product entity.</returns>
        Task<Product> CreateAsync(Product product);


        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product entity if found; otherwise, null.</returns>
        Task<Product> GetAsync(int id);


        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all product entities.</returns>
        Task<List<Product>> GetAllAsync();



        /*Task<Product> UpdateAsync(int id, Product product, IEnumerable<int> categoryIds);*/


        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">The product entity with updated data.</param>
        /// <returns>The updated product entity.</returns>
        Task<Product> UpdateAsync(Product product);


        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>True if deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);



        /// <summary>
        /// Retrieves all products that match query.
        /// </summary>
        /// <param name="query">The query by which products are searched.</param>
        /// <returns>A list of matching product entities that match the query.</returns>
        Task<List<Product>> SearchProductsAsync(string? query);


    }
}
