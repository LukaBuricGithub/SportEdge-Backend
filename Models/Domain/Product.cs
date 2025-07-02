using System.ComponentModel.DataAnnotations;

namespace SportEdge.API.Models.Domain
{
    /// <summary>
    /// Domain model for representing a product.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique identifier for the brand.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Product name (required, max 150 characters).
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }


        /// <summary>
        /// Product short description (required, max 300 characters).
        /// </summary>
        [Required]
        [MaxLength(300)]
        public string ShortDescription { get; set; }


        /// <summary>
        /// Product price
        /// </summary>
        public decimal Price { get; set; }


        /// <summary>
        /// Discounted price
        /// </summary>
        public decimal? DiscountedPrice { get; set; }


        //Novo zakomentirano
        //public Gender Gender { get; set; }
        //public int GenderId { get; set; }


        /// <summary>
        /// Navigation property for the brand.
        /// </summary>
        public Brand Brand { get; set; }

        /// <summary>
        /// The ID of the brand.
        /// </summary>
        public int BrandId { get; set; }



        /// <summary>
        /// Navigation property for the gender.
        /// </summary>
        public Gender Gender { get; set; }


        /// <summary>
        /// The ID of the gender.
        /// </summary>
        public int GenderId { get; set; }




        /// <summary>
        /// List of categories associated with this product.
        /// </summary>
        public ICollection<Category> Categories { get; set; } = new List<Category>();


        /// <summary>
        /// List of product variations associated with this product.
        /// </summary>
        public ICollection<ProductVariation> ProductVariations { get; set; } = new List<ProductVariation>();


        /// <summary>
        /// List of product images associated with this product.
        /// </summary>
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    }
}
