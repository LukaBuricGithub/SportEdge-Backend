using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;

namespace SportEdge.API.Mappings
{
    /// <summary>
    /// Provides mapping functionality between Cart item domain models and DTOs.
    /// </summary>
    public class CartItemMapping
    {
        /// <summary>
        /// Maps a Cart item domain model to a CartItemDto.
        /// </summary>
        /// <param name="cartItem">The domain model to convert.</param>
        /// <returns>A CartItemDto containing the mapped data.</returns>
        public CartItemDto ToDto(CartItem cartItem)
        {
            return new CartItemDto()
            {
                Id = cartItem.Id,
                CartId = cartItem.CartId,
                ProductVariationId = cartItem.ProductVariationId,
                ProductName = cartItem.ProductVariation.Product.Name,
                SizeOptionName = cartItem.ProductVariation.SizeOption.SizeName,
                PriceAtTime = cartItem.ProductVariation.Product.Price,
                Quantity = cartItem.Quantity

            };
        }
    }
}
