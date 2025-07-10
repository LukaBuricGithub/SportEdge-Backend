namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO representing a filtered products.
    /// </summary>
    public class FilteredProductsResultDto
    {
        /// <summary>
        /// The list of filtered products.
        /// </summary>
        public List<ProductDto> Products { get; set; }

        /// <summary>
        /// Total number of products.
        /// </summary>
        public int TotalCount { get; set; }
    }
}
