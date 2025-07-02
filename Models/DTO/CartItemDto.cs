namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO representing a cart item.
    /// </summary>
    public class CartItemDto
    {

        /// <summary>
        /// Unique identifier for the cart item.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// The ID of the cart.
        /// </summary>
        public int CartId { get; set; }


        /// <summary>
        /// Name of the product.
        /// </summary>
        public string ProductName { get; set; }


        /// <summary>
        /// The ID of the product variation.
        /// </summary>
        public int ProductVariationId { get; set; }

        /// <summary>
        /// The name of size option.
        /// </summary>
        public string SizeOptionName { get; set; }


        /// <summary>
        /// Price of cart item.
        /// </summary>
        public decimal PriceAtTime { get; set; }


        /// <summary>
        /// Quantity of cart item.
        /// </summary>
        public int Quantity { get; set; }
    }
}
