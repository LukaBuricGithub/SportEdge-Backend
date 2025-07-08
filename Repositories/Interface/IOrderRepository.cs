using SportEdge.API.Models.Domain;

namespace SportEdge.API.Repositories.Interface
{
    /// <summary>
    /// Interface for order-related data operations.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Adds a new order to the database.
        /// </summary>
        /// <param name="order">The order entity to create.</param>
        /// <returns>The created order entity.</returns>
        Task<Order> CreateAsync(Order order);


        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>A list of all order entities.</returns>
        Task<List<Order>> GetAllAsync();


        /// <summary>
        /// Retrieves an order by its ID.
        /// </summary>
        /// <param name="id">The ID of an order to retrieve.</param>
        /// <returns>The order entity if found; otherwise, null.</returns>
        Task<Order> GetAsync(int id);

        /// <summary>
        /// Retrieves all orders for user.
        /// <param name="userId">The user ID.</param>
        /// </summary>
        /// <returns>A list of all order entities for a user.</returns>
        Task<List<Order>> GetAllByUserIdAsync(int userId);


        /// <summary>
        /// Checks if there is an order item with given list of productVariationId.
        /// </summary>
        /// <param name="productVariationIds">The list of productVariationId to check from.</param>
        /// <returns>True if found at least one order item; otherwise, false.</returns>
        Task<bool> AnyOrderItemContainsVariationsAsync(List<int> productVariationIds);


    }
}
