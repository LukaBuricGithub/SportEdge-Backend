using SportEdge.API.Configuration;
using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO used when creating a new user.
    /// </summary>
    public class CreateUserRequestDto
    {

        /// <summary>
        /// First name of the user (required, 1-100 characters).
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string FirstName { get; set; }


        /// <summary>
        /// Last name of the user (required, 1-100 characters).
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; }


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
        [PasswordComplexity(ErrorMessage = "Password does not meet the complexity requirements.")]
        public string Password { get; set; }


        /// <summary>
        ///  Date of birth for user (required).
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }


        /// <summary>
        /// Country of the user (required, 1-100 characters).
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Country { get; set; }

        /// <summary>
        /// City of the user (required, 1-100 characters).
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string City { get; set; }

        /// <summary>
        /// Address of the user (required, 1-100 characters).
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Address { get; set; }
    }
}
