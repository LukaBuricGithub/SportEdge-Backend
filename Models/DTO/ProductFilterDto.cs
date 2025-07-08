namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO representing a filter for searching products.
    /// </summary>
    public class ProductFilterDto
    {
        /// <summary>
        /// List of category IDs to search from.
        /// </summary>
        public List<int>? CategoryIds { get; set; }

        /// <summary>
        /// Gender ID.
        /// </summary>
        public int? GenderId { get; set; }
        
        /// <summary>
        /// Brand ID.
        /// </summary>
        public int? BrandId { get; set; }

        /// <summary>
        /// MinPrice to search from.
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// MaxPrice to search from.
        /// </summary>
        public decimal? MaxPrice { get; set; } 
    }
}
