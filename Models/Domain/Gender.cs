using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.Domain
{
    /// <summary>
    /// Domain model for representing a gender.
    /// </summary>
    public class Gender
    {
        /// <summary>
        /// Unique identifier for the gender.
        /// </summary>
        public int Id {  get; set; }


        /// <summary>
        /// Gender name (required, max 100 characters).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }


        /// <summary>
        /// List of size options associated with this gender.
        /// </summary>
        public ICollection<SizeOption> SizeOptions { get; set; } = new List<SizeOption>();


        /// <summary>
        /// List of products associated with this gender.
        /// </summary>
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
