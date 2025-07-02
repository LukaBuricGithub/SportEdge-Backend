using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportEdge.API.Login;
using SportEdge.API.Models.DTO;
using SportEdge.API.Services.Implementation;
using SportEdge.API.Services.Interface;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace SportEdge.API.Controllers
{
    /// <summary>
    /// API controller for managing user-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService) 
        {
            this.userService = userService;
        }


        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="request">The user data.</param>
        /// <returns>Ok if user created.</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequestDto request)
        {
            try
            {
                var newUser = await userService.CreateAsync(request);
                return Ok(new {message = "Successfully created new user." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Used to login user.
        /// </summary>
        /// <param name="request">The login user data.</param>
        /// <returns>Ok if user is successfully logged in; NotFound or BadRequest.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserRequestDto request)
        {
            try
            {
                var loggedUser = await userService.LoginRequest(request);
                return Ok(new LoginResponseDto { Token = loggedUser });
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Generates a password reset token for the user.
        /// </summary>
        /// <param name="request">The request containing the user's email.</param>
        /// <returns>Ok if the email is found and if reset token is sent; otherwise, NotFound.</returns>
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] UserForgotPasswordRequestDto request)
        {
            try
            {
                await userService.GeneratePasswordResetTokenAsync(request);
                return Ok(new { message = "Reset token sent to a user!" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Resets the user's password using reset token and new password.
        /// </summary>
        /// <param name="request">The request containing the reset token and the new password.</param>
        /// <returns>Ok if the password is successfully reset; otherwise, BadRequest.</returns>
        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordRequestDto request)
        {
            try
            {
                await userService.ResetPasswordAsync(request);
                return Ok(new { message = "Password successfully reset." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Sends the message a user wrote using customer service form.
        /// </summary>
        /// <param name="request">The request containing information about message.</param>
        /// <returns>Ok if the message was sent successfully; otherwise, BadRequest.</returns>
        [AllowAnonymous]
        [HttpPost("customer-service-request")]
        public async Task<IActionResult> CustomerServiceSendMessage([FromBody]UserSendMessageDto request) 
        { 
            try 
            { 
                await userService.CustomerServiceSendMessageAsync(request);
                return Ok(new { message = "Message sent to customer service." });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userService.GetAllAsync();
            if (!users.Any())
            {
                return Ok(new List<UserDto>());
            }
            return Ok(users);
        }


        /// <summary>
        /// Retrieves a specific user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The requested user if found; otherwise, NotFound.</returns>
        /// <returns>Unauthorized or Forbid if users credentials don't match; the requested if found; otherwise, NotFound.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Admin");


            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userIdFromToken))
            {

                return Unauthorized("Invalid token.");
            }

            if (!isAdmin && userIdFromToken != id)
            {
                return Forbid("You can only update your own account.");
            }

            try
            {
                var user = await userService.GetAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Updates an existing user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="request">The updated user data.</param>
        /// <returns>Unauthorized or Forbid if users credentials don't match; the updated user if update is successful; otherwise, NotFound.</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody]UpdateUserRequestDto request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Admin");


            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userIdFromToken))
            { 
                
                 return Unauthorized("Invalid token.");
            }

            if (!isAdmin && userIdFromToken != id)
            {
                return Forbid("You can only update your own account.");
            }

            try
            {
                var updatedUser = await userService.UpdateAsync(id, request);
                return Ok(updatedUser);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }



        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>No content if successful; otherwise, NotFound.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var isDeleted = await userService.DeleteAsync(id);
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
