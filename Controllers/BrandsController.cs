using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportEdge.API.Data;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;
using SportEdge.API.Repositories.Interface;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Controllers
{

    /// <summary>
    /// API controller for managing brand-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandsController(IBrandService brandService) 
        {
            this.brandService = brandService;
        }



        /// <summary>
        /// Creates a new brand.
        /// </summary>
        /// <param name="request">The brand data.</param>
        /// <returns>The created brand with a location header.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody]CreateBrandRequestDto request)
        {   
            var brand = await brandService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetBrand),
                new { id = brand.Id },
                brand);
        }


        /// <summary>
        /// Retrieves all brands.
        /// </summary>
        /// <returns>A list of all brands.</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllBrands() 
        { 
            var brands = await brandService.GetAllAsync();
            if (!brands.Any())
            {
                return Ok(new List<BrandDto>());
            }
            return Ok(brands);
        }


        /// <summary>
        /// Retrieves a specific brand by ID.
        /// </summary>
        /// <param name="id">The ID of the brand.</param>
        /// <returns>The requested brand if found; otherwise, NotFound.</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrand(int id)
        {
            try 
            {
                var brand = await brandService.GetAsync(id);
                return Ok(brand);
            }
            catch (KeyNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Searches for brands by name.
        /// </summary>
        /// <param name="searchTerm">The search term to use for filtering brands.</param>
        /// <returns>A list of matching brands.</returns>
        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> GetBrandsByName([FromQuery] string? searchTerm = null)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                var brands = await brandService.GetAllAsync();
                if (!brands.Any())
                {
                    return Ok(new List<BrandDto>());
                }
                return Ok(brands);

            }
            var foundBrands = await brandService.GetBrandsByNameAsync(searchTerm);
            return Ok(foundBrands);
        }


        /// <summary>
        /// Updates an existing brand by ID.
        /// </summary>
        /// <param name="id">The ID of the brand to update.</param>
        /// <param name="request">The updated brand data.</param>
        /// <returns>The updated brand if successful; otherwise, NotFound.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, [FromBody]UpdateBrandRequestDto request)
        {
            try
            {
                var brand = await brandService.UpdateAsync(id, request);
                return Ok(brand);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Deletes a brand by ID.
        /// </summary>
        /// <param name="id">The ID of the brand to delete.</param>
        /// <returns>No content if successful; otherwise, NotFound or BadRequest.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                var isDeleted = await brandService.DeleteAsync(id);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
