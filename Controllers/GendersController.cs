using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;
using SportEdge.API.Services.Implementation;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Controllers
{
    /// <summary>
    /// API controller for managing gender-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly IGenderService genderService;

        public GendersController(IGenderService genderService)
        {
            this.genderService = genderService;
        }


        /// <summary>
        /// Creates a new gender.
        /// </summary>
        /// <param name="request">The gender data.</param>
        /// <returns>The created gender with a location header.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateGender([FromBody]CreateGenderRequestDto request)
        {
            var gender = await genderService.CreateAsync(request);
            
            return CreatedAtAction(
            nameof(GetGender),
               new { id = gender.Id },
               gender);
        }


        /// <summary>
        /// Retrieves all genders.
        /// </summary>
        /// <returns>A list of all genders.</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllGenders()
        {
            var genders = await genderService.GetAllAsync();

            if (!genders.Any())
            {
                return Ok(new List<GenderDto>());
            }
            return Ok(genders);
        }


        /// <summary>
        /// Retrieves a specific gender by ID.
        /// </summary>
        /// <param name="id">The ID of the gender.</param>
        /// <returns>The requested gender if found; otherwise, NotFound.</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGender(int id)
        {
            try
            {
                var gender = await genderService.GetAsync(id);
                return Ok(gender);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Updates an existing gender by ID.
        /// </summary>
        /// <param name="id">The ID of the gender to update.</param>
        /// <param name="request">The updated gender data.</param>
        /// <returns>The updated gender if successful; otherwise, NotFound.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGender(int id, [FromBody]UpdateGenderRequestDto request)
        {
            try
            {
                var updatedGender = await genderService.UpdateAsync(id, request);
                return Ok(updatedGender);

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Deletes a gender by ID.
        /// </summary>
        /// <param name="id">The ID of the gender to delete.</param>
        /// <returns>No content if successful; otherwise, NotFound.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender(int id)
        {
            try
            {
                var isDeleted = await genderService.DeleteAsync(id);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }        
        }
    }
}
