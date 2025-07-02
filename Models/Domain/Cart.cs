namespace SportEdge.API.Models.Domain
{
    /// <summary>
    /// Domain model for representing a cart.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Unique identifier for the cart.
        /// </summary>
        public int Id { get; set; }



        /// <summary>
        /// Navigation property for the user.
        /// </summary>
        public User User { get; set; }


        /// <summary>
        /// User Id; user is owner of a cart.
        /// </summary>
        public int UserId { get; set; } // nullable for guest users


        /// <summary>
        /// Session Id which is used to identify cart; this is used when user is not logged in (can be null).
        /// </summary>
        //public string? SessionId { get; set; }


        /// <summary>
        /// Date when cart is created.
        /// </summary>
        public DateTime CreatedAt { get; set; }


        /// <summary>
        /// List of cart items associated with cart.
        /// </summary>
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
