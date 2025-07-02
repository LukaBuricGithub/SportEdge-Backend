using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;

namespace SportEdge.API.Mappings
{

    /// <summary>
    /// Provides mapping functionality between ProductVariation domain models and DTOs.
    /// </summary>
    public class ProductVariationMapping
    {


        /// <summary>
        /// Maps a CreateProductVariationRequestDto to a ProductVariation domain model.
        /// </summary>
        /// <param name="productVariation">The DTO containing data for creating a product variation.</param>
        /// <returns>A new ProductVariation domain model instance.</returns>
        public ProductVariation ToDomain(CreateProductVariationRequestDto productVariation)
        {
            return new ProductVariation()
            {
                QuantityInStock = productVariation.QuantityInStock,
                ProductId = productVariation.ProductId,
                SizeOptionId = productVariation.SizeOptionId
            };
        }

        /// <summary>
        /// Maps an UpdateProductVariationRequestDto to a ProductVariation domain model.
        /// </summary>
        /// <param name="productVariation">The DTO containing updated product variation data.</param>
        /// <returns>A ProductVariation domain model instance with updated values.</returns>
        public ProductVariation ToDomain(UpdateProductVariationRequestDto productVariation)
        {
            return new ProductVariation()
            {
                QuantityInStock = productVariation.QuantityInStock,
                ProductId = productVariation.ProductId,
                SizeOptionId = productVariation.SizeOptionId
            };
        }

        /// <summary>
        /// Maps a ProductVariation domain model to a ProductVariationDto.
        /// </summary>
        /// <param name="productVariation">The domain model to convert.</param>
        /// <returns>A ProductVariationDto containing the mapped data.</returns>
        public ProductVariationDto ToDto(ProductVariation productVariation) 
        {
            return new ProductVariationDto()
            {
                Id = productVariation.Id,
                QuantityInStock = productVariation.QuantityInStock,
                ProductId = productVariation.ProductId,
                ProductName = productVariation.Product?.Name,
                SizeOptionId = productVariation.SizeOptionId,
                SizeOptionName = productVariation.SizeOption?.SizeName,
            };
        }
    }
}
