using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Controllers
{

    /// <summary>
    /// API controller for managing product image-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService productImageService;
        public ProductImagesController(IProductImageService productImageService) 
        { 
            this.productImageService = productImageService;
        }



        /// <summary>
        /// Uploads new product images for a product.
        /// </summary>
        /// <param name="files">New product images.</param>
        /// <param name="productId">The ID of a product.</param>
        /// <returns>Ok if images were uploaded; otherwise, BadRequest.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("{productId}")]
        public async Task<IActionResult> UploadImages(int productId, [FromForm] IFormFileCollection files)
        {
            try
            {
                await productImageService.UploadImagesAsync(productId, files);
                return Ok(new { message = "Successfully uploaded images." });
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Updates product with new images.
        /// </summary>
        /// <param name="files">New product images.</param>
        /// <param name="productId">The ID of a product.</param>
        /// <returns>Ok if images were uploaded; otherwise, BadRequest.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateImages(int productId, [FromForm] IFormFileCollection files)
        {
            try
            {
                await productImageService.UpdateImagesAsync(productId, files);
                return Ok(new { message = "Successfully uploaded images." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }




        /// <summary>
        /// Deletes all product images by product ID.
        /// </summary>
        /// <param name="productId">The ID of the product whose images are to delete.</param>
        /// <returns>No content if successful; otherwise, NotFound.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteAllImages(int productId)
        {
            try
            {
                await productImageService.DeleteImagesAsync(productId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all product images for a product.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>The requested images if found; otherwise, NotFound.</returns>
        [AllowAnonymous]
        [HttpGet("{productId}/images")]
        public async Task<IActionResult> GetImages(int productId)
        {
            try
            {
                var imageUrls = await productImageService.GetImageUrlsAsync(productId);
                return Ok(imageUrls);
            }

            catch (KeyNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }

        }

    }
}
