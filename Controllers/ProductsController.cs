using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;
using SportEdge.API.Services.Implementation;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Controllers
{

    /// <summary>
    /// API controller for managing product-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }


        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="request">The product data.</param>
        /// <returns>The created product with a location header; otherwise, BadRequest</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]CreateProductRequestDto request)
        {
            try
            {
                var product = await productService.CreateAsync(request);

                return CreatedAtAction(
                    nameof(GetProduct),
                    new { id = product.Id },
                    product);
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }   
        }


        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productService.GetAllAsync();

            if(!products.Any())
            {
                return Ok(new List<ProductDto>());
            }
            return Ok(products);
        }


        /// <summary>
        /// Retrieves a specific products by ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The requested product if found; otherwise, NotFound.</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await productService.GetAsync(id);
                return Ok(product);
            }
            catch (KeyNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }  
        }

        /// <summary>
        /// Searches for products by search query.
        /// </summary>
        /// <param name="searchTerm">The search term to use for filtering products.</param>
        /// <returns>A list of matching products.</returns>
        [AllowAnonymous]
        [HttpPost("search")]
        public async Task<IActionResult> SearchProduct([FromBody] ProductFilterDto filterDto,[FromQuery] string? searchTerm = null)
        {
            var products = await productService.SearchProductsAsync(searchTerm, filterDto);
            //if (!products.Any())
            //{
            //    return Ok(new List<ProductDto>());
            //}
            return Ok(products);
        }



        /// <summary>
        /// Searches for products by filter model.
        /// </summary>
        /// <param name="filterDto">The filter model containing filter data.</param>
        /// <returns>A list of matching products.</returns>
        [AllowAnonymous]
        [HttpPost("filter")]
        public async Task<IActionResult> FilterProducts([FromBody] ProductFilterDto filterDto)
        {
            var productsFiltered = await productService.FilterProductsAsync(filterDto);
            //if (!products.Any())
            //{
                //return Ok(new List<ProductDto>());
            //}
            return Ok(productsFiltered);
        }


        /// <summary>
        /// Searches for products that have a categoryId as in entry parameter.
        /// </summary>
        /// <param name="categoryId">The categoryId you want products to have.</param>
        /// <returns>A list of  products that have matching categoryId.</returns>
        [AllowAnonymous]
        [HttpPost("search-category")]
        public async Task<IActionResult> GetProductByCategory(int? categoryId, [FromBody] ProductFilterDto filterDto) 
        {
            if (!categoryId.HasValue || categoryId<=0) 
            {
                return BadRequest(new { message = "Please enter a positive number for categoryId." });
            }
            var products = await productService.GetProductsByCategoryIdAsync(categoryId.Value,filterDto);
            //if (!products.Any())
            //{
                //return Ok(new List<ProductDto>());
            //}
            return Ok(products);
   
        }


        [AllowAnonymous]
        [HttpPost("filter-products")]
        public async Task<IActionResult> GetFilteredProducts([FromBody] ProductFilterWithTextDto filterDto) 
        {
            var filteredProducts = await productService.GetFilteredProductsAsync(filterDto);
            return Ok(filteredProducts);
        }


        /// <summary>
        /// Searches for products by their gender type.
        /// </summary>
        /// <param name="name">The search term to use for filtering products by gender type.</param>
        /// <returns>A list of matching products.</returns>
        [AllowAnonymous]
        [HttpPost("search-gender-type")]
        public async Task<IActionResult> GetProductsByGenderTypeAsync(string name, [FromBody] ProductFilterDto filterDto) 
        {
            try 
            {
                var products = await productService.GetProductsByGenderTypeAsync(name, filterDto);
                return Ok(products);
            
            }
            catch (InvalidDataException ex) 
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Updates an existing product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="request">The updated product data.</param>
        /// <returns>The updated product if successful; otherwise, NotFound or BadRequest</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,[FromBody]UpdateProductRequestDto request)
        {
            try
            {
                var updatedProduct = await productService.UpdateAsync(id, request);
                return Ok(updatedProduct);
            }
            catch (KeyNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
            catch(InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>No content if successful; otherwise, NotFound.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var isDeleted = await productService.DeleteAsync(id);
                return NoContent();
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
    }
}
