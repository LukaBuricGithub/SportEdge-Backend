using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportEdge.API.Models.DTO;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Controllers
{
    /// <summary>
    /// API controller for managing size option-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SizeOptionsController : ControllerBase
    {
        private readonly ISizeOptionService sizeOptionService;

        public SizeOptionsController(ISizeOptionService sizeOptionService)
        {
            this.sizeOptionService = sizeOptionService;
        }


        /// <summary>
        /// Creates a new size option.
        /// </summary>
        /// <param name="request">The size option data.</param>
        /// <returns>The created size option with a location header.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSizeOption([FromBody] CreateSizeOptionRequestDto request)
        {
           var sizeOption = await sizeOptionService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetSizeOption),
                new { id = sizeOption.Id },
                sizeOption);      
        }


        /// <summary>
        /// Retrieves all size options.
        /// </summary>
        /// <returns>A list of all size options.</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllSizeOptions()
        {
            var sizeOptions = await sizeOptionService.GetAllAsync();
            if (!sizeOptions.Any())
            {
                return Ok(new List<SizeOptionDto>());
            }

            return Ok(sizeOptions);
        }


        /// <summary>
        /// Retrieves a specific size option by ID.
        /// </summary>
        /// <param name="id">The ID of the size option.</param>
        /// <returns>The requested size option if found; otherwise, NotFound.</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSizeOption(int id)
        {
            try
            {
                var sizeOption = await sizeOptionService.GetAsync(id);
                return Ok(sizeOption);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }  
        }


        /// <summary>
        /// Retrieves all size options for specific gender.
        /// </summary>
        /// <param name="genderId">The ID of the gender.</param>
        /// <returns>A list of all size options for gender.</returns>
        [HttpGet("by-gender/{genderId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSizeOptionsForGender(int genderId)
        {
            var sizeOptions = await sizeOptionService.GetSizeOptionsByGenderIdAsync(genderId);
            if (!sizeOptions.Any())
            {
                return Ok(new List<SizeOptionDto>());
            }

            return Ok(sizeOptions);
        }



        /// <summary>
        /// Updates an existing size option by ID.
        /// </summary>
        /// <param name="id">The ID of the size option to update.</param>
        /// <param name="request">The updated size option data.</param>
        /// <returns>The updated size option if successful; otherwise, NotFound.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSizeOption(int id, [FromBody] UpdateSizeOptionRequestDto request)
        {
            try
            {
                var updatedSizeOption = await sizeOptionService.UpdateAsync(id, request);
                return Ok(updatedSizeOption);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);

            }    
        }


        /// <summary>
        /// Deletes a size option by ID.
        /// </summary>
        /// <param name="id">The ID of the size option to delete.</param>
        /// <returns>No content if successful; otherwise, NotFound.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSizeOption(int id)
        {
            try
            {
                var isDeleted = await sizeOptionService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
