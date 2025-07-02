using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    ///DTO representing a request when user forgets password.
    /// </summary>
    public class UserForgotPasswordRequestDto
    {
        /// <summary>
        /// Email of the user (required).
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
    }
}
