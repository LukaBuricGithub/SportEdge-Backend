namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO representing an order.
    /// </summary>
    public class OrderItemDto
    {
        /// <summary>
        /// Unique identifier for the order item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID of product variation.
        /// </summary>
        public int ProductVariationId { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Item size option.
        /// </summary>
        public string SizeOptionName { get; set; }

        /// <summary>
        /// Number of items.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Price of one item unit.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Total price of item.
        /// </summary>
        public decimal SubtotalPrice { get; set; }

    }
}
