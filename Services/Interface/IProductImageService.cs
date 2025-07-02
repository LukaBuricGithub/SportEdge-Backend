namespace SportEdge.API.Services.Interface
{
    /// <summary>
    /// Defines service operations for managing product images.
    /// </summary>
    public interface IProductImageService
    {
        /// <summary>
        /// Uploads images to a product.
        /// </summary>
        /// <param name="productId">ID of a product.</param>
        /// <param name="images"> Uploaded images.</param>  
        Task UploadImagesAsync(int productId, IFormFileCollection images);

        /// <summary>
        /// Deletes all images from the product.
        /// </summary>
        /// <param name="productId">The ID of the product whose images will be deleted.</param>
        Task DeleteImagesAsync(int productId);


        /// <summary>
        /// Returns the path/location to images for a product.
        /// </summary>
        /// <param name="productId">ID of a product.</param>
        /// <returns>List of product images (the location where they are saved).</returns>
        Task<List<string>> GetImageUrlsAsync(int productId);



        /// <summary>
        /// Updates images for the product.
        /// </summary>
        /// <param name="productId">The ID of the product whose images will be updated.</param>
        /// <param name="newImages"> New updated images.</param>  
        Task UpdateImagesAsync(int productId, IFormFileCollection newImages);


    }
}
