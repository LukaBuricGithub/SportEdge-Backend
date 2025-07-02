using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO used when creating a new gender.
    /// </summary>
    public class CreateGenderRequestDto
    {
        /// <summary>
        /// Gender name (required, 2–100 characters).
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
