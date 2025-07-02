using SportEdge.API.Models.DTO;

namespace SportEdge.API.Services.Interface
{
    /// <summary>
    /// Defines service operations for managing order.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Creates a new order for a user.
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="request">DTO containing data for the new order.</param>
        /// <returns>The created order as a DTO.</returns>
        Task<OrderDto> PlaceOrderAsync(int userId, CreateOrderRequestDto request);


        /// <summary>
        /// Retrieves an order by its ID.
        /// </summary>
        /// <param name="id">The order ID.</param>
        /// <returns>The order DTO.</returns>
        Task<OrderDto> GetAsync(int id);

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>The list of order DTOs.</returns>
        Task<List<OrderDto>> GetAllAsync();

        /// <summary>
        /// Retrieves all orders by its owner (userId).
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The list of order DTOs.</returns>
        Task<List<OrderDto>> GetAllByUserIdAsync(int userId);
    }
}
