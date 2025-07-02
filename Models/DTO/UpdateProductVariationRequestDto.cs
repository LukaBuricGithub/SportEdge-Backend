using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO used when updating an existing product variation.
    /// </summary>
    public class UpdateProductVariationRequestDto
    {

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
