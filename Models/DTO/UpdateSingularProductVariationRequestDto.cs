using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// Alternative DTO used when updating an existing product variation.
    /// </summary>
    public class UpdateSingularProductVariationRequestDto
    {
        /// <summary>
        /// Unique identifier for the product variation (required).
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Amount of product variation in stock (required) .
        /// </summary>
        [Required]
        public int QuantityInStock { get; set; }


        /// <summary>
        /// Assigned product ID (required).
        /// </summary>
        [Required]
        public int ProductId { get; set; }



        /// <summary>
        /// Assigned size option ID (required).
        /// </summary>
        [Required]
        public int SizeOptionId { get; set; }
    }
}
