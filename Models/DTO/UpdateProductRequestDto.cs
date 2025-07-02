using SportEdge.API.Configuration;
using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO used when updating an existing product.
    /// </summary>
    public class UpdateProductRequestDto
    {
        /// <summary>
        /// Product name (required, 2-150 characters).
        /// </summary>
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; }


        /// <summary>
        /// Product short description (required, 2-300 characters).
        /// </summary>
        [Required]
        [StringLength(300, MinimumLength = 2)]
        public string ShortDescription { get; set; }


        /// <summary>
        /// Product price (required).
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Discounted price
        /// </summary>
        public decimal? DiscountedPrice { get; set; }


        /// <summary>
        /// Assigned brand ID (required).
        /// </summary>
        [Required]
        public int BrandId { get; set; }


        /// <summary>
        /// Assigned categories ID (required).
        /// </summary>
        [Required]
        [NotEmptyList(ErrorMessage = "At least one category must be selected.")]
        public List<int> CategoryIds { get; set; }
    }
}
