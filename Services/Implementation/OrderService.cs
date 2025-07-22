using SportEdge.API.Mappings;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;
using SportEdge.API.Repositories.Implementation;
using SportEdge.API.Repositories.Interface;
using SportEdge.API.Services.Interface;

namespace SportEdge.API.Services.Implementation
{

    /// <summary>
    /// Provides implementation for order-related service operations.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICartRepository cartRepository;
        private readonly IProductVariationRepository productVariationRepository;
        private readonly OrderMapping mapping;
        private readonly CartMapping cartMapping;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IProductVariationRepository productVariationRepository, 
            OrderMapping mapping, CartMapping cartMapping)
        {
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
            this.productVariationRepository = productVariationRepository;
            this.mapping = mapping;
            this.cartMapping = cartMapping;
        }



        /// <inheritdoc/>
        public async Task<OrderDto> PlaceOrderAsync(int userId, CreateOrderRequestDto request)
        {
            var cart = await cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null || !cart.CartItems.Any())
            {
                throw new ArgumentException("Cart is empty.");
            }

            var cartDto = cartMapping.ToDto(cart);

            var orderItems = new List<OrderItem>();
            var variationsToUpdate = new List<ProductVariation>();

            foreach (var cartItemDto in cartDto.CartItems)
            {
                var productVariation = await productVariationRepository.GetAsync(cartItemDto.ProductVariationId);

                if (productVariation == null)
                {
                    throw new KeyNotFoundException($"Product variation with ID {cartItemDto.ProductVariationId} not found.");
                }

                if (productVariation.QuantityInStock < cartItemDto.Quantity)
                {
                    throw new InvalidOperationException($"Not enough items in stock for your order of product: {productVariation.Product.Name} (size {productVariation.SizeOption.SizeName}).");
                }

                orderItems.Add(new OrderItem
                {
                    ProductVariationId = cartItemDto.ProductVariationId,
                    Quantity = cartItemDto.Quantity,
                    UnitPrice = cartItemDto.PriceAtTime
                });

                productVariation.QuantityInStock -= cartItemDto.Quantity;
                variationsToUpdate.Add(productVariation);
            }

            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UserCountry = request.UserCountry,
                UserCity = request.UserCity,
                UserAddress = request.UserAddress,
                OrderItems = orderItems
            };

            // using var transaction = await dbContext.Database.BeginTransactionAsync();

            foreach (var variation in variationsToUpdate)
            {
                await productVariationRepository.UpdateAsync(variation);
            }

            await orderRepository.CreateAsync(order);
            await cartRepository.ClearCartAsync(cart.Id);
            await cartRepository.DeleteAsync(cart.Id);

            // await transaction.CommitAsync();

            return mapping.ToDto(order);
        }


        /// <inheritdoc/>
        public async Task<OrderDto> GetAsync(int id)
        {
            var order = await orderRepository.GetAsync(id);
            if(order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");

            }
            return mapping.ToDto(order);
        }

        /// <inheritdoc/>
        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await orderRepository.GetAllAsync();
            return orders.Select(o => mapping.ToDto(o)).ToList();
        }

        /// <inheritdoc/>
        public async Task<List<OrderDto>> GetAllByUserIdAsync(int userId)
        {
            var orders = await orderRepository.GetAllByUserIdAsync(userId);
            return orders.Select(o => mapping.ToDto(o)).ToList();
        }

    }
}
