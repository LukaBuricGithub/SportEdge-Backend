using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportEdge.API.Models.Domain;
using SportEdge.API.Models.DTO;
using SportEdge.API.Services.Implementation;
using SportEdge.API.Services.Interface;
using System.Security.Claims;

namespace SportEdge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> PlaceOrderAsync(int userId, [FromBody]CreateOrderRequestDto request)
        //{
        //    try
        //    {
        //        var order = await orderService.PlaceOrderAsync(userId,request);

        //        return CreatedAtAction(
        //            nameof(GetOrder),new { id = order.Id },order);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="request">Request which contains extra data for creating an order (country,city,address).</param>
        /// <returns>The information about the newly created order.</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PlaceOrderAsync([FromBody] CreateOrderRequestDto request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userIdFromToken))
            {
                return Unauthorized("Invalid token.");
            }

            try
            {
                var order = await orderService.PlaceOrderAsync(userIdFromToken, request);

                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("Exceeded_quantity: " + ex.Message);
            }
            catch (KeyNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex) 
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>List of all orders orders.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await orderService.GetAllAsync();
            if (!orders.Any())
            {
                return Ok(new List<OrderDto>());
            }
            return Ok(orders);
        }


        /// <summary>
        /// Retrieves all orders from the user.
        /// </summary>
        /// <param name="userId">The Id the user whose orders need to be retrieved.</param>
        /// <returns>List of user's orders.</returns>
        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllOrdersByUserId(int userId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Admin");

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userIdFromToken))
            {
                return Unauthorized("Invalid token.");
            }

            if (!isAdmin && userIdFromToken != userId)
            {
                return Forbid("You can only access your own orders.");
            }

            var orders = await orderService.GetAllByUserIdAsync(userId);

            if (!orders.Any())
            {
                return Ok(new List<OrderDto>());
            }

            return Ok(orders);
        }




        /// <summary>
        /// Retrieves an order by its Id.
        /// </summary>
        /// <param name="id">The Id of an order to retrieve.</param>
        /// <returns>Order information.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Admin");

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userIdFromToken))
            {
                return Unauthorized("Invalid token.");
            }

            try
            {
                var order = await orderService.GetAsync(id);

                if (!isAdmin && order.UserId != userIdFromToken)
                {
                    return Forbid("You can only access your own orders.");
                }

                return Ok(order);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }



        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOrder(int id)
        //{
        //    try
        //    {
        //        var order = await orderService.GetAsync(id);
        //        return Ok(order);
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

    }
}
