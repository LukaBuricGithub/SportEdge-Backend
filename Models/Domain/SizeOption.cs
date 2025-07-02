using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.Domain
{
    /// <summary>
    /// Domain model for representing a size option.
    /// </summary>
    public class SizeOption
    {
        /// <summary>
        /// Unique identifier for the size option.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Size option name (required, max 100 characters).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string SizeName { get; set; }



        /// <summary>
        /// Navigation property for the gender.
        /// </summary>
        public Gender Gender { get; set; }


        /// <summary>
        /// The ID of the gender.
        /// </summary>
        public int GenderId { get; set; }


        /// <summary>
        /// List of product variations associated with size option.
        /// </summary>
        public ICollection<ProductVariation> ProductVariations { get; set; } = new List<ProductVariation>();

    }
}
