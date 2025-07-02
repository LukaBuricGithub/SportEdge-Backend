using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Controllers
{
    /// <summary>
    /// API controller for managing product variation option-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariationsController : ControllerBase
    {
        private readonly IProductVariationService productVariationService;

        public ProductVariationsController(IProductVariationService productVariationService)
        {
            this.productVariationService = productVariationService;
        }


        /// <summary>
        /// Creates a new product variation.
        /// </summary>
        /// <param name="request">The product variation data.</param>
        /// <returns>The created product variation option with a location header.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateProductVariation (CreateProductVariationRequestDto request)
        {
            var productVariation = await productVariationService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetProductVariation),
                new { id = productVariation.Id },
                productVariation);
        }


        /// <summary>
        /// Retrieves all product variations.
        /// </summary>
        /// <returns>A list of all product variations.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProductVariations()
        {
            var productVariations = await productVariationService.GetAllAsync();
            if (!productVariations.Any())
            {
                return Ok(new List<ProductVariationDto>());
            }
            return Ok(productVariations);
        }


        /// <summary>
        /// Retrieves a specific product variation by ID.
        /// </summary>
        /// <param name="id">The ID of the product variation.</param>
        /// <returns>The requested product variation if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductVariation(int id)
        {
            try
            {
                var productVariation = await productVariationService.GetAsync(id);
                return Ok(productVariation);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing product variation by ID.
        /// </summary>
        /// <param name="id">The ID of the product variation to update.</param>
        /// <param name="request">The updated product variation data.</param>
        /// <returns>The updated product variation if successful; otherwise, NotFound.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductVariation(int id, [FromBody]UpdateProductVariationRequestDto request)
        {
            try
            {
                var updatedProductVariation = await productVariationService.UpdateAsync(id, request);
                return Ok(updatedProductVariation);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);

            }
        }
    }
}
