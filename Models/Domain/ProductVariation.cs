namespace SportEdge.API.Models.Domain
{
    /// <summary>
    /// Domain model for representing a product variation.
    /// </summary>
    public class ProductVariation
    {
        /// <summary>
        /// Unique identifier for the product variation.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Quantity of product variation in stock.
        /// </summary>
        public int QuantityInStock { get; set; }



        /// <summary>
        /// Navigation property for the size option.
        /// </summary>
        public SizeOption SizeOption { get; set; }


        /// <summary>
        /// The ID of the size option.
        /// </summary>
        public int SizeOptionId { get; set; }


        /// <summary>
        /// Navigation property for the product.
        /// </summary>
        public Product Product { get; set; }


        /// <summary>
        /// The ID of the product.
        /// </summary>
        public int ProductId { get; set; }


        /// <summary>
        /// List of cart items associated with this product variation.
        /// </summary>
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();


        /// <summary>
        /// List of order items associated with this product variation.
        /// </summary>
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
