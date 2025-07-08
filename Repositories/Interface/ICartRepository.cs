using SportEdge.API.Models.Domain;

namespace SportEdge.API.Repositories.Interface
{
    /// <summary>
    /// Interface for cart-related data operations.
    /// </summary>
    public interface ICartRepository
    {

        /// <summary>
        /// Retrieves a cart by its owner (userId).
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The cart entity if found.</returns>
        Task<Cart> GetCartByUserIdAsync(int userId);


        /// <summary>
        /// Adds a new cart to the database for the user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The created cart entity.</returns>
        Task<Cart> CreateCartAsync(int userId);


        /// <summary>
        /// Adds a new item to the cart.
        /// </summary>
        /// <param name="cartId">The cart ID.</param>
        /// <param name="productVariationId">Id of added item.</param>
        /// <param name="quantity">Quantity of added item.</param>
        /// <returns>True if item is added successfully; otherwise false.</returns>
        Task<bool> AddItemToCartAsync(int cartId, int productVariationId, int quantity);


        /// <summary>
        /// Removes an item from the cart.
        /// </summary>
        /// <param name="cartId">The cart ID.</param>
        /// <param name="productVariationId">Id of removed item.</param>
        /// <returns>True if item is removed successfully; otherwise false.</returns>
        Task<bool> RemoveItemFromCartAsync(int cartId, int productVariationId);


        /// <summary>
        /// Updates the quantity of an item in a cart.
        /// </summary>
        /// <param name="cartId">The cart ID.</param>
        /// <param name="productVariationId">Id of updated item.</param>
        /// <param name="newQuantity">New quantity of item.</param>
        /// <returns>True if item is updated successfully; otherwise false.</returns>
        Task<bool> UpdateCartItemQuantityAsync(int cartId, int productVariationId, int newQuantity);


        /// <summary>
        /// Clears items from cart.
        /// </summary>
        /// <param name="cartId">The cart ID.</param>
        /// <returns>True if cart is cleared successfully; otherwise false.</returns>
        Task<bool> ClearCartAsync(int cartId);


        /// <summary>
        /// Deletes a cart by its ID.
        /// </summary>
        /// <param name="cartId">The ID of the cart to delete.</param>
        /// <returns>True if the cart was deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteAsync(int cartId);


        /// <summary>
        /// Checks if there is a cart item with given list of productVariationId.
        /// </summary>
        /// <param name="productVariationIds">The list of productVariationId to check from.</param>
        /// <returns>True if found at least one cart item; otherwise, false.</returns>
        Task<bool> AnyCartItemContainsVariationsAsync(List<int> productVariationIds);

    }
}
