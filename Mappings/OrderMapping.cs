using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;

namespace SportEdge.API.Mappings
{
    /// <summary>
    /// Provides mapping functionality between Order domain models and DTOs.
    /// </summary>
    public class OrderMapping
    {
        /// <summary>
        /// Maps an Order domain model to an OrderDto.
        /// </summary>
        /// <param name="order">The domain model to convert.</param>
        /// <returns>An OrderDto containing the mapped data.</returns>
        public OrderDto ToDto(Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                UserCountry = order.UserCountry,
                UserCity = order.UserCity,
                UserAddress = order.UserAddress,
                TotalPrice = order.OrderItems?.Sum(oi => oi.Quantity * oi.UnitPrice) ?? 0,
                OrderItems = order.OrderItems?.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    ProductVariationId = oi.ProductVariationId,
                    ProductName = oi.ProductVariation.Product.Name,
                    SizeOptionName = oi.ProductVariation.SizeOption.SizeName,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity,
                    SubtotalPrice = oi.UnitPrice * oi.Quantity
                }).ToList()
            };
        }
    }
}
