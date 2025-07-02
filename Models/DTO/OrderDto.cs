namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO representing an order.
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// Unique identifier for the order.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Order owner ID.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Date when order was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }


        /// <summary>
        /// List of order items.
        /// </summary>
        public List<OrderItemDto> OrderItems { get; set; }

        /// <summary>
        /// Total price of the order.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Country of the user.
        /// </summary>
        public string UserCountry { get; set; }

        /// <summary>
        /// City of the user.
        /// </summary>
        public string UserCity { get; set; }

        /// <summary>
        /// Address of the user.
        /// </summary>
        public string UserAddress { get; set; }

    }
}
