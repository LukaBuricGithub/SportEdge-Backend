using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.Domain
{
    /// <summary>
    /// Domain model for representing an order.
    /// </summary>
    public class Order
    {

        /// <summary>
        /// Unique identifier for the order.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Date when order was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        /// <summary>
        /// Total price of the order.
        /// </summary>
        //public decimal TotalPrice { get; set; }

        /// <summary>
        /// Navigation property for the user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// User Id; user is owner of an order.
        /// </summary>
        public int UserId { get; set; }


        /// <summary>
        /// List of order items associated with order.
        /// </summary>
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


        /// <summary>
        /// Country of the user (required, max 100 characters).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string UserCountry { get; set; }

        /// <summary>
        /// City of the user (required, max 100 characters).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string UserCity { get; set; }

        /// <summary>
        /// Address of the user (required, max 100 characters).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string UserAddress { get; set; }


    }
}
