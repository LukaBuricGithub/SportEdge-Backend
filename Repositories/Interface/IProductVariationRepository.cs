using SportEdge.API.Models.Domain;

namespace SportEdge.API.Repositories.Interface
{
    /// <summary>
    /// Interface for product variation-related data operations.
    /// </summary>
    public interface IProductVariationRepository
    {

        /// <summary>
        /// Adds a new product variation to the database.
        /// </summary>
        /// <param name="productVariation">The product variation entity to create.</param>
        /// <returns>The created product variation entity.</returns>
        Task<ProductVariation> CreateAsync(ProductVariation productVariation);


        /// <summary>
        /// Retrieves a product variation by its ID.
        /// </summary>
        /// <param name="id">The ID of the product variation to retrieve.</param>
        /// <returns>The product variation entity if found; otherwise, null.</returns>
        Task<ProductVariation> GetAsync(int id);


        /// <summary>
        /// Retrieves all product variations.
        /// </summary>
        /// <returns>A list of all product variation entities.</returns>
        Task<List<ProductVariation>> GetAllAsync();



        /// <summary>
        /// Updates an existing product variation.
        /// </summary>
        /// <param name="productVariation">The product variation entity with updated data.</param>
        /// <returns>The updated product variation entity.</returns>
        Task<ProductVariation> UpdateAsync(ProductVariation productVariation);

    }
}
