using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.Domain
{
    /// <summary>
    /// Domain model for representing a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// First name of the user (required, max 100 characters).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }


        /// <summary>
        /// Last name of the user (required, max 100 characters).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }


        /// <summary>
        /// Email of the user (required, max 100 characters).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Represents if user is admin (admin - true; not admin - false).
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Password of the user (required).
        /// </summary>
        [Required]
        public string Password { get; set; }



        /// <summary>
        /// Country of the user (required, max 100 characters).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        /// <summary>
        /// City of the user (required, max 100 characters).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        /// <summary>
        /// Address of the user (required, max 100 characters).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }


        /// <summary>
        /// Date of birth for user.
        /// </summary>
        public DateTime DateOfBirth { get; set; }


        /// <summary>
        /// Token for reseting password (can be null).
        /// </summary>
        public string? PasswordResetToken { get; set; }

        /// <summary>
        /// Expiration date for password reset token (can be null).
        /// </summary>
        public DateTime? ResetTokenExpiration { get; set; }



        //public ICollection<Cart> Carts { get; set; } = new List<Cart>();


        /// <summary>
        /// Navigation property for the cart.
        /// </summary>
        public Cart Cart { get; set; }


        /// <summary>
        /// List of orders with this user.
        /// </summary>
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
