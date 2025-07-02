using SportEdge.API.Models.DTO;

namespace SportEdge.API.Services.Interface
{
    /// <summary>
    /// Defines service operations for managing cart.
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Retrieves a cart by its owner (userId).
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The cart DTO.</returns>
        Task<CartDto> GetCartByUserIdAsync(int userId);

        /// <summary>
        /// Adds an item into cart.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="productVariationId">Id of added item.</param>
        /// <param name="quantity">Quantity of added item.</param>
        Task AddItemToCartAsync(int userId, int productVariationId, int quantity);


        /// <summary>
        /// Removes an item from a cart.
        /// </summary>
        /// <param name="userId">The user Id (owner of a cart).</param>
        /// <param name="productVariationId">Id of removed item.</param>
        Task RemoveItemFromCartAsync(int userId, int productVariationId);



        /// <summary>
        /// Updates the quantity of item in cart.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="productVariationId">Id of updated item.</param>
        /// <param name="newQuantity">New quantity of item.</param>
        Task UpdateCartItemQuantityAsync(int userId, int productVariationId, int newQuantity);

        /// <summary>
        /// Cleares users cart.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        Task ClearCartAsync(int userId);


        /// <summary>
        /// Deletes a cart by its ID.
        /// </summary>
        /// <param name="cartId">The ID of the cart to delete.</param>
        /// <returns>True if the cart was deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteAsync(int cartId);

    }
}
