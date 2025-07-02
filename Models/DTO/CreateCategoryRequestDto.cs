using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO used when creating a new category.
    /// </summary>
    public class CreateCategoryRequestDto
    {

        /// <summary>
        /// Category name (required, 2-150 characters).
        /// </summary>
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// Parent category's ID (can be null).
        /// </summary>
        public int? ParentCategoryId { get; set; }
    }
}
