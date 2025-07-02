using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO used when updating an existing user.
    /// </summary>
    public class UpdateUserRequestDto
    {
        /// <summary>
        /// First name of the user (required, 1-100 characters).
        /// </summary>
        [Required]
        [StringLength(100,MinimumLength = 1)]
        public string Firstname { get; set; }


        /// <summary>
        /// Last name of the user (required, 1-100 characters).
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; }


        /// <summary>
        /// Date of birth for user (required).
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
