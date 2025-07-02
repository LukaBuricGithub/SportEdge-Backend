using SportEdge.API.Configuration;
using SportEdge.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO used when creating a new order.
    /// </summary>
    public class CreateOrderRequestDto
    {
        /// <summary>
        /// Country of the user (required, 1-100 characters).
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string UserCountry { get; set; }

        /// <summary>
        /// City of the user (required, 1-100 characters).
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string UserCity { get; set; }

        /// <summary>
        /// Address of the user (required, 1-100 characters).
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string UserAddress { get; set; }

        /*
        /// <summary>
        /// User Id; user is owner of an order.
        /// </summary>
        [Required]
        public int UserId { get; set; }
        */


        /*
        /// <summary>
        /// Assigned categories ID (required).
        /// </summary>
        [Required]
        [NotEmptyList(ErrorMessage = "At least one order item must be selected.")]
        public List<int> OrderItemsIds { get; set; }
        */


        /*
        /// <summary>
        /// Date when order was created.
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }
        */
    }
}
