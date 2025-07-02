namespace SportEdge.API.Models.Domain
{
    /// <summary>
    /// Domain model for representing a cart item.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Unique identifier for the cart item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Navigation property for the cart.
        /// </summary>
        public Cart Cart { get; set; }


        /// <summary>
        /// The ID of cart.
        /// </summary>
        public int CartId { get; set; }


        /// <summary>
        /// Navigation property for the product variation.
        /// </summary>
        public ProductVariation ProductVariation { get; set; }

        /// <summary>
        /// The ID of the product variation.
        /// </summary>
        public int ProductVariationId { get; set; }


        /// <summary>
        /// Quantity of cart item.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Price of cart item.
        /// </summary>
        public decimal PriceAtTime { get; set; }
    }
}
