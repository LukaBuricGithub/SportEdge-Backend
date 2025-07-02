using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO used when updating an existing gender.
    /// </summary>
    public class UpdateGenderRequestDto
    {
        /// <summary>
        /// Gender name (required, 2-100 characters).
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
