using SportEdge.API.Mappings;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;
using SportEdge.API.Repositories.Interface;
using SportEdge.API.Services.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace SportEdge.API.Services.Implementation
{

    /// <summary>
    /// Provides implementation for cart-related service operations.
    /// </summary>
    public class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;
        private readonly IProductVariationRepository productVariationRepository;
        private readonly CartMapping mapping;

        public CartService(ICartRepository cartRepository, CartMapping mapping, IProductVariationRepository productVariationRepository)
        {
            this.cartRepository = cartRepository;
            this.mapping = mapping;
            this.productVariationRepository = productVariationRepository;
        }


        /// <inheritdoc/>
        public async Task<CartDto> GetCartByUserIdAsync(int userId)
        {

            var cart = await cartRepository.GetCartByUserIdAsync(userId) ?? await cartRepository.CreateCartAsync(userId);
            return mapping.ToDto(cart);

        }

        /// <inheritdoc/>
        public async Task AddItemToCartAsync(int userId, int productVariationId, int quantity)
        {
            var cart = await cartRepository.GetCartByUserIdAsync(userId) ?? await cartRepository.CreateCartAsync(userId);
            
            var productVariation = await productVariationRepository.GetAsync(productVariationId);

            if (productVariation == null)
            {
                throw new KeyNotFoundException($"Product variation with ID {productVariationId} not found.");
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductVariationId == productVariationId);
            int existingQuantity = existingItem?.Quantity ?? 0;

            if (existingQuantity + quantity > productVariation.QuantityInStock) 
            {
                throw new InvalidOperationException($"Cannot add {quantity} items. Only {productVariation.QuantityInStock - existingQuantity} more in stock.");
            }


            await cartRepository.AddItemToCartAsync(cart.Id, productVariationId, quantity);
        }


        /// <inheritdoc/>
        public async Task RemoveItemFromCartAsync(int userId, int productVariationId)
        {
            var cart = await cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                throw new KeyNotFoundException($"Cart not found for user with ID {userId}");
            }

            var removed = await cartRepository.RemoveItemFromCartAsync(cart.Id, productVariationId);
            if (!removed)
            {
                throw new InvalidOperationException($"Cannot remove product variation item with ID {productVariationId} from cart.");
            }
        }


        /// <inheritdoc/>
        public async Task UpdateCartItemQuantityAsync(int userId, int productVariationId, int newQuantity)
        {
            var cart = await cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                throw new KeyNotFoundException($"Cart not found for user with ID {userId}");
            }

            var productVariation = await productVariationRepository.GetAsync(productVariationId);
            if (productVariation == null)
            {
                throw new KeyNotFoundException($"Product variation with ID {productVariationId} not found.");
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductVariationId == productVariationId);
            if (existingItem == null)
            {
                throw new InvalidOperationException($"Item with variation ID {productVariationId} not found in the cart.");
            }

            if (newQuantity > productVariation.QuantityInStock)
            {
                throw new InvalidOperationException($"Cannot have {newQuantity} items in cart.");

            }

            
            var updated = await cartRepository.UpdateCartItemQuantityAsync(cart.Id, productVariationId, newQuantity);
            if (!updated)
            {
                throw new InvalidOperationException($"Cannot update product variation item with ID {productVariationId}.");
            }
        
        }

        /// <inheritdoc/>
        public async Task ClearCartAsync(int userId) 
        {
            var cart = await cartRepository.GetCartByUserIdAsync(userId);
            if(cart == null)
            {
                throw new KeyNotFoundException($"Cart not found for user with ID {userId}");
            }

            var cleared = await cartRepository.ClearCartAsync(cart.Id);
            if (!cleared)
            {
                throw new InvalidOperationException($"No items found in cart with ID {cart.Id} to clear.");
            }
        }

        public async Task<bool> DeleteAsync(int cartId)
        {
            var deleted = await cartRepository.DeleteAsync(cartId);
            if (!deleted)
            {
                throw new KeyNotFoundException($"Cart with ID {cartId} not found.");
            }
            return true;
        }

    }
}
