using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;

namespace SportEdge.API.Mappings
{
    /// <summary>
    /// Provides mapping functionality between Cart domain models and DTOs.
    /// </summary>
    public class CartMapping
    {
        /// <summary>
        /// Maps a Cart domain model to a CartDto.
        /// </summary>
        /// <param name="cart">The domain model to convert.</param>
        /// <returns>A CartDto containing the mapped data.</returns>
        public CartDto ToDto(Cart cart)
        {
            return new CartDto()
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CreatedAt = cart.CreatedAt,
                CartItems = cart.CartItems?.Select(ci => new CartItemDto
                {
                    Id = ci.Id,
                    CartId = ci.CartId,
                    ProductVariationId = ci.ProductVariationId,
                    ProductName = ci.ProductVariation.Product.Name,
                    SizeOptionName = ci.ProductVariation.SizeOption.SizeName,
                    PriceAtTime = ci.ProductVariation.Product.DiscountedPrice ?? ci.ProductVariation.Product.Price,
                    //PriceAtTime = ci.PriceAtTime,
                    //PriceAtTime = ci.ProductVariation.Product.Price,
                    Quantity = ci.Quantity,
                    ProductId = ci.ProductVariation.Product.Id
                }).ToList()
            };
        }
    }
}
