using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO used when sending an email message with optional PDF attachment (e.g., order receipt).
    /// </summary>
    public class UserSendReceiptDto
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
        /// Subject of the email (required).
        /// </summary>
        [Required]
        public string Subject { get; set; }

        /// <summary>
        /// Body/content of the email (required, 1-500 characters).
        /// </summary>
        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Content { get; set; }

        /// <summary>
        /// Base64 encoded PDF file to attach (optional).
        /// </summary>
        public string? PdfBase64 { get; set; }

    }
}
