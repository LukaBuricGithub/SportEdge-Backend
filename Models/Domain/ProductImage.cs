using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.Domain
{
    /// <summary>
    /// Domain model for representing a product image.
    /// </summary>
    public class ProductImage
    {
        /// <summary>
        /// Unique identifier for the product image.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Product image file name (required).
        /// </summary>
        [Required]
        public string Filename { get; set; }


        /// <summary>
        /// The ID of the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Navigation property for the product.
        /// </summary>
        public Product Product { get; set; }
    }
}
