using SportEdge.API.Configuration;
using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    ///DTO representing a request when user uses token to create a new password.
    /// </summary>
    public class UserResetPasswordRequestDto
    {
        /// <summary>
        /// Received reset token (required).
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// New password (required).
        /// </summary>
        [PasswordComplexity(ErrorMessage = "Password does not meet the complexity requirements.")]
        [Required]
        public string NewPassword { get; set; } 
    }
}
