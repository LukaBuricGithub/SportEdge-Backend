namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO representing a product.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Unique identifier for the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product short description.
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Product price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Discounted price
        /// </summary>
        public decimal? DiscountedPrice { get; set; }

        /// <summary>
        /// Assigned brand ID.
        /// </summary>
        public int BrandId { get; set; }


        /// <summary>
        /// Product brand name.
        /// </summary>
        public string BrandName {  get; set; }


        /// <summary>
        /// List of product categories (their names).
        /// </summary>
        public List<string> CategoryNames { get; set; }

        /// <summary>
        /// List of product categories (their IDs).
        /// </summary>
        public List<int> CategoryIds { get; set; }


        /// <summary>
        /// List of product variations.
        /// </summary>
        public List<ProductVariationDto> Variations { get; set; }

        /// <summary>
        /// Quantity of product in stock.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// List of product images.
        /// </summary>
        public List<string> ImageFilenames { get; set; }


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
