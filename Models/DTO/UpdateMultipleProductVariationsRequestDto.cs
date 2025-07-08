namespace SportEdge.API.Models.DTO
{
    /// <summary>
    /// DTO representing a request when updating multiple product variations at once.
    /// </summary>
    public class UpdateMultipleProductVariationsRequestDto
    {
        /// <summary>
        /// List of product variations to update.
        /// </summary>
        public List<UpdateSingularProductVariationRequestDto> Variations { get; set; } = new();
    }
}
