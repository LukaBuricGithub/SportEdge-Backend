using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO used when user tries to login.
    /// </summary>
    public class LoginUserRequestDto
    {
        /// <summary>
        /// Email of the user (required).
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }


        /// <summary>
        /// Password of the user (required).
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
