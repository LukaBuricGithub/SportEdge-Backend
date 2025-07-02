namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO representing a cart.
    /// </summary>
    public class CartDto
    {
        /// <summary>
        /// Unique identifier for the cart.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// User Id; user is owner of a cart.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Session Id which is used to identify cart; this is used when user is not logged in (can be null).
        /// </summary>
        //public string? SessionId { get; set; }
        
        
        /// <summary>
        /// Date when cart is created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// List of cart items.
        /// </summary>
        public List<CartItemDto>  CartItems { get; set; }

    }
}
