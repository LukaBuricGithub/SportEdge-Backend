using Microsoft.EntityFrameworkCore;
using SportEdge.API.Data;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;
using SportEdge.API.Repositories.Interface;

namespace SportEdge.API.Repositories.Implementation
{
    /// <summary>
    /// Repository implementation for cart-related data operations.
    /// </summary>
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CartRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<Cart> CreateCartAsync(int userId)
        {
            var cart = new Cart { UserId = userId, CartItems = new List<CartItem>(), CreatedAt = DateTime.UtcNow };
            await dbContext.Carts.AddAsync(cart);
            await dbContext.SaveChangesAsync();
            return cart;
        }



        /// <inheritdoc/>
        public async Task<bool> AddItemToCartAsync(int cartId, int productVariationId, int quantity)
        {
            var cart = await dbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null)
            {
                return false;
            }

            var productVariation = await dbContext.ProductVariations
                    .Include(pv => pv.Product)
                    .FirstOrDefaultAsync(pv => pv.Id == productVariationId);


 

            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductVariationId == productVariationId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }

            else
            {
                var price = productVariation.Product.DiscountedPrice ?? productVariation.Product.Price;

                cart.CartItems.Add(new CartItem
                {
                    CartId = cartId,
                    ProductVariationId = productVariationId,
                    Quantity = quantity,
                    PriceAtTime = price
                });

            }

            await dbContext.SaveChangesAsync();
            return true;
        }



        /// <inheritdoc/>
        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
   
            return await dbContext.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariation)
                        .ThenInclude(pv => pv.SizeOption)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariation.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

        }

        /// <inheritdoc/>
        public async Task<bool> ClearCartAsync(int cartId)
        {
            var items = await dbContext.CartItems.Where(ci => ci.CartId == cartId).ToListAsync();

            if (!items.Any())
            {
                return false;
            }

            dbContext.CartItems.RemoveRange(items);
            await dbContext.SaveChangesAsync();
            return true;
        }



        /// <inheritdoc/>
        public async Task<bool> RemoveItemFromCartAsync(int cartId, int productVariationId)
        {
            var item = await dbContext.CartItems
                    .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductVariationId == productVariationId);

            if (item == null)
            {
                return false;
            }

            dbContext.CartItems.Remove(item);
            await dbContext.SaveChangesAsync();
            return true;

        }


        /// <inheritdoc/>
        public async Task<bool> UpdateCartItemQuantityAsync(int cartId, int productVariationId, int newQuantity)
        {

            var item = await dbContext.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductVariationId == productVariationId);

            if (item == null) 
            {
                return false;
            }

            item.Quantity = newQuantity;
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int cartId)
        {
            var cart = await dbContext.Carts.FindAsync(cartId);
            if (cart == null)
            {
                return false;
            }

            dbContext.Carts.Remove(cart);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
