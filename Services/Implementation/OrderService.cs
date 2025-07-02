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

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IProductVariationRepository productVariationRepository, OrderMapping mapping)
        {
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
            this.productVariationRepository = productVariationRepository;
            this.mapping = mapping;
        }



        /// <inheritdoc/>
        public async Task<OrderDto> PlaceOrderAsync(int userId,CreateOrderRequestDto request)
        {
            var cart = await cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null || !cart.CartItems.Any())
            {
                throw new InvalidOperationException("Cart is empty.");
            }

            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UserCountry = request.UserCountry,
                UserCity = request.UserCity,
                UserAddress = request.UserAddress,
                OrderItems = new List<OrderItem>()
            };


            foreach (var item in cart.CartItems)
            {
                var productVariation = await productVariationRepository.GetAsync(item.ProductVariationId);
                
                if (productVariation == null)
                {
                    throw new InvalidOperationException($"Product variation with ID {item.ProductVariationId} not found.");
                }

                if (productVariation.QuantityInStock < item.Quantity)
                {
                    throw new InvalidOperationException($"Not enough stock for product variation with ID {productVariation.Id}.");
                }

                productVariation.QuantityInStock -= item.Quantity;
                await productVariationRepository.UpdateAsync(productVariation);

                var orderItem = new OrderItem
                {
                    ProductVariationId = item.ProductVariationId,
                    Quantity = item.Quantity,
                    UnitPrice = item.PriceAtTime
                    //UnitPrice = item.ProductVariation.Product.Price 
                };

                order.OrderItems.Add(orderItem);
            }

            await orderRepository.CreateAsync(order);
            await cartRepository.ClearCartAsync(cart.Id);
            await cartRepository.DeleteAsync(cart.Id);

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
