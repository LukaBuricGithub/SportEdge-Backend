using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO used when user sends a message.
    /// </summary>
    public class UserSendMessageDto
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
        /// Subject of the message (required).
        /// </summary>
        [Required]
        public string Subject { get; set; }


        /// <summary>
        /// Content of the message (required, 1-500 characters).
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Content { get; set; }

    }
}