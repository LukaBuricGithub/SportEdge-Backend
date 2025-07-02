using SportEdge.API.Models.Domain;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO representing a size option.
    /// </summary>
    public class SizeOptionDto
    {
        /// <summary>
        /// Unique identifier for the size option.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Size option name.
        /// </summary>
        public string SizeName { get; set; }

        /// <summary>
        /// Assigned gender ID.
        /// </summary>
        public int GenderId { get; set; }

        /// <summary>
        /// Assigned gender name.
        /// </summary>
        public string GenderName { get; set; }
    }
}
