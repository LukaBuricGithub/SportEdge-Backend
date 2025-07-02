using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.Domain
{
    /// <summary>
    /// Domain model for representing a brand.
    /// </summary>
    public class Brand
    {
        /// <summary>
        /// Unique identifier for the brand.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Brand name (required, max 100 characters).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// List of products associated with brand.
        /// </summary>
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
