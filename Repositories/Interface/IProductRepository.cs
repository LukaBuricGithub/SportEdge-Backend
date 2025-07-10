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
        /// <param name="filter">Filter object that has pagination options.</param>
        /// <returns>A list of matching product entities that match the query.</returns>
        Task<FilteredProductsResultDomain> SearchProductsAsync(string? query, ProductFilterDto filter);


        /// <summary>
        /// Retrieves all products that have the gender type in query.
        /// </summary>
        /// <param name="name">The name of gender type you are looking for.</param>
        /// <param name="filter">Filter object that has pagination options.</param>
        /// <returns>A list of matching product entities that match the query.</returns>
        Task<FilteredProductsResultDomain> GetProductsByGenderTypeAsync(string name, ProductFilterDto filter);


        /// <summary>
        /// Retrieves all products that match the given filter object.
        /// </summary>
        /// <param name="filter">The filter object used for searching products.</param>
        /// <returns>The list of matching products.</returns>
        //Task<List<Product>> FilterProductsAsync(ProductFilterDto filter);
        Task<FilteredProductsResultDomain> FilterProductsAsync(ProductFilterDto filter);




        /// <summary>
        /// Retrieves all products that have categoryId from the query.
        /// </summary>
        /// <param name="categoryId">The categoryId you want products to have.</param>
        /// <param name="filter">Filter object that has pagination options.</param>
        /// <returns>The list of matching products.</returns>
        //Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<FilteredProductsResultDomain> GetProductsByCategoryIdAsync(int categoryId,ProductFilterDto filter);



        /// <summary>
        /// Retrieves all products that match the given filter object.
        /// </summary>
        /// <param name="filter">The filter object used for searching products.</param>
        /// <returns>The list of matching products.</returns>
        Task<FilteredProductsResultDomain> GetFilteredProductsAsync(ProductFilterWithTextDto filter);
    }
}
