using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO used when creating a new brand.
    /// </summary>
    public class CreateBrandRequestDto
    {
        /// <summary>
        /// Brand name (required, 2-100 characters).
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
