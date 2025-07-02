using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;
using SportEdge.API.Repositories.Implementation;
using SportEdge.API.Repositories.Interface;
using SportEdge.API.Services.Implementation;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Controllers
{
    /// <summary>
    /// API controller for managing category-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }


        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="request">The category data.</param>
        /// <returns>The created category with a location header.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryRequestDto request)
        {
            var category = await categoryService.CreateAsync(request);

            return Ok(new { message = "Successfully created new category." });

            //return CreatedAtAction(
            //nameof(GetCategory),
            //new { id = category.Id },
            //category);
        }


        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of all categories.</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryService.GetAllAsync();
            if (!categories.Any())
            {
                return Ok(new List<CategoryDto>());
            }
            return Ok(categories);
        }


        /// <summary>
        /// Retrieves a specific category by ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>The requested category if found; otherwise, NotFound.</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id) 
        {
            try
            {
                var category = await categoryService.GetAsync(id);
                return Ok(category);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Updates an existing category by ID.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="request">The updated category data.</param>
        /// <returns>The updated category if successful; otherwise, NotFound or BadRequest.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody]UpdateCategoryRequestDto request)
        {
            try
            {
                var updatedCategory = await categoryService.UpdateAsync(id, request);
                return Ok(updatedCategory);
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
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>No content if successful; otherwise, NotFound or BadRequest.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var isDeleted = await categoryService.DeleteAsync(id);
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
