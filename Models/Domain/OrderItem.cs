namespace SportEdge.API.Models.Domain
{
    /// <summary>
    /// Domain model for representing an order item.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Unique identifier for the order item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Navigation property for the order.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// The ID of order.
        /// </summary>
        public int OrderId { get; set; }


        /// <summary>
        /// Navigation property for the product variation.
        /// </summary>
        public ProductVariation ProductVariation { get; set; }


        /// <summary>
        /// The ID of product variation.
        /// </summary> 
        public int ProductVariationId { get; set; }


        /// <summary>
        /// Price of order item.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Quantity of order item.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Total price.
        /// </summary>
        //public decimal Subtotal => UnitPrice * Quantity;


        //public string ProductName { get; set; }        
        //public string SizeName { get; set; } 
    }
}
