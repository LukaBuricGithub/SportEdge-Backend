using SportEdge.API.Models.Domain;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO representing a product variation.
    /// </summary>
    public class ProductVariationDto
    {
        /// <summary>
        /// Unique identifier for the product variation.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Amount of product variation in stock.
        /// </summary>
        public int QuantityInStock { get; set; }

        /// <summary>
        /// Assigned product ID.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Assigned product name.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Assigned size option ID.
        /// </summary>
        public int SizeOptionId { get; set; }

        /// <summary>
        /// Assigned size option name.
        /// </summary>
        public string SizeOptionName { get; set; }
    }
}
