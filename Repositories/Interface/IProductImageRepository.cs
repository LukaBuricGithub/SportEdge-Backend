using SportEdge.API.Models.Domain;

namespace SportEdge.API.Repositories.Interface
{
    /// <summary>
    /// Interface for product image-related data operations.
    /// </summary>
    public interface IProductImageRepository
    {

        /// <summary>
        /// Retrieves all product images associated with a specific product ID.
        /// </summary>
        /// <param name="productId">Product ID.</param>
        Task<IEnumerable<ProductImage>> GetByProductIdAsync (int productId);



        /// <summary>
        /// Uploads image files into folder and saves their destination in the database.
        /// </summary>
        /// <param name="images">Uploaded image files.</param>
        Task AddImagesAsync (List<ProductImage> images);


        /// <summary>
        /// Deletes all product images associated with a specific product ID.
        /// </summary>
        /// <param name="productId">Product ID.</param>
        Task DeleteByProductIdAsync (int productId);

    }
}
