using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{

    /// <summary>
    /// DTO used when creating a new size option.
    /// </summary>
    public class CreateSizeOptionRequestDto
    {

        /// <summary>
        /// Size option name (required, 1-100 characters).
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string SizeName { get; set; }


        /// <summary>
        /// Assigned gender ID (required).
        /// </summary>
        [Required]
        public int GenderId { get; set; }
    }
}
