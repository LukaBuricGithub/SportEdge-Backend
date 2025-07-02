using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO used when updating an existing category.
    /// </summary>
    public class UpdateCategoryRequestDto
    {
        /// <summary>
        /// Category's name (required, 2-150 characters).
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
