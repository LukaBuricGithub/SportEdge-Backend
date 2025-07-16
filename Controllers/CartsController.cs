using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportEdge.API.Services.Interface;
using System.Security.Claims;

namespace SportEdge.API.Controllers
{


    /// <summary>
    /// API controller for managing cart-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartsController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        /// <summary>
        /// Adds a new item to a cart.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <param name="productVariationId">Id of added product.</param>
        /// <param name="quantity">Number of added items.</param>
        /// <returns>Ok if item is added into cart; otherwise NotFound or BadRequest.</returns>
        [Authorize]
        [HttpPost("{userId}/items")]
        public async Task<IActionResult> AddItem(int userId, int productVariationId, int quantity)
        {
            var (userIdFromToken, isAdmin, errorResult) = GetUserContext(userId);
            if (errorResult != null) return errorResult;

            try
            {
                await cartService.AddItemToCartAsync(userId, productVariationId, quantity);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves user cart which is found by userId.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <returns>Data for user cart.</returns>
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var (userIdFromToken, isAdmin, errorResult) = GetUserContext(userId);
            if (errorResult != null) return errorResult;

            var cart = await cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        /// <summary>
        /// Updates a quantity of an item in a cart.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="productVariationId">Id of updated item.</param>
        /// <param name="quantity">New quantity of the item.</param>
        /// <returns>Ok if update was successful; otherwise, NotFound or BadRequest.</returns>
        [Authorize]
        [HttpPut("{userId}/items/{productVariationId}")]
        public async Task<IActionResult> UpdateItem(int userId, int productVariationId, int quantity)
        {
            var (userIdFromToken, isAdmin, errorResult) = GetUserContext(userId);
            if (errorResult != null) return errorResult;

            try
            {
                await cartService.UpdateCartItemQuantityAsync(userId, productVariationId, quantity);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes an item from a cart.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="productVariationId">Id of deleted item.</param>
        /// <returns>Ok if delete action was successful; otherwise, NotFound or BadRequest.</returns>
        [Authorize]
        [HttpDelete("{userId}/items/{productVariationId}")]
        public async Task<IActionResult> RemoveItem(int userId, int productVariationId)
        {
            var (userIdFromToken, isAdmin, errorResult) = GetUserContext(userId);
            if (errorResult != null) return errorResult;

            try
            {
                await cartService.RemoveItemFromCartAsync(userId, productVariationId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Clears user cart.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Ok if cart was cleared successfully; otherwise, NotFound or BadRequest.</returns>
        [Authorize]
        [HttpDelete("{userId}/items")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var (userIdFromToken, isAdmin, errorResult) = GetUserContext(userId);
            if (errorResult != null) return errorResult;

            try
            {
                await cartService.ClearCartAsync(userId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Helper method to get user context and validate access.
        /// </summary>
        private (int userIdFromToken, bool isAdmin, IActionResult? errorResult) GetUserContext(int userId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Admin");

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userIdFromToken))
            {
                return (0, false, Unauthorized("Invalid token."));
            }

            if (!isAdmin && userIdFromToken != userId)
            {
                return (0, false, Forbid("You can only access your own cart."));
            }

            return (userIdFromToken, isAdmin, null);
        }

        ///// <summary>
        ///// Adds a new item to a cart.
        ///// </summary>
        ///// <param name="userId">User Id.</param>
        ///// <param name="productVariationId">Id of added product.</param>
        ///// <param name="quantity">Number of added items.</param>
        ///// <returns>Ok if item is added into cart; otherwise NotFound or BadRequest.</returns>
        //[Authorize]
        //[HttpPost("{userId}/items")]
        //public async Task<IActionResult> AddItem(int userId, int productVariationId, int quantity)
        //{
        //    try
        //    {
        //        await cartService.AddItemToCartAsync(userId, productVariationId, quantity);
        //        return Ok();
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Retrieves user cart which is found by userId.
        ///// </summary>
        ///// <param name="userId">User Id.</param>
        ///// <returns>Data for user cart.</returns>
        //[Authorize]
        //[HttpGet("{userId}")]
        //public async Task<IActionResult> GetCart(int userId)
        //{
        //    var cart = await cartService.GetCartByUserIdAsync(userId);
        //    return Ok(cart);

        //}

        ///// <summary>
        ///// Updates a quantity of an item in a cart.
        ///// </summary>
        ///// <param name="userId">User id.</param>
        ///// <param name="productVariationId">Id of updated item.</param>
        ///// <param name="quantity">New quantity of a item.</param>
        ///// <returns>Ok if updating item was successful; otherwise, NotFound or BadRequest.</returns>
        //[Authorize]
        //[HttpPut("{userId}/items/{productVariationId}")]
        //public async Task<IActionResult> UpdateItem(int userId, int productVariationId, int quantity)
        //{
        //    try
        //    {
        //        await cartService.UpdateCartItemQuantityAsync(userId, productVariationId, quantity);
        //        return Ok();
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        ///// <summary>
        ///// Deletes an item from a cart.
        ///// </summary>
        ///// <param name="userId">User id.</param>
        ///// <param name="productVariationId">Id of deleted item.</param>
        ///// <returns>Ok if delete action was successful; otherwise, NotFound or BadRequest.</returns>
        //[Authorize]
        //[HttpDelete("{userId}/items/{productVariationId}")]
        //public async Task<IActionResult> RemoveItem(int userId, int productVariationId)
        //{
        //    try
        //    {
        //        await cartService.RemoveItemFromCartAsync(userId, productVariationId);
        //        return Ok();
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        ///// <summary>
        ///// Clears user cart.
        ///// </summary>
        ///// <param name="userId">User id.</param>
        ///// <returns>Ok if cart was cleared successfully; otherwise, NotFound or BadRequest.</returns>
        //[Authorize]
        //[HttpDelete("{userId}/items")]
        //public async Task<IActionResult> ClearCart(int userId)
        //{
        //    try
        //    {
        //        await cartService.ClearCartAsync(userId);
        //        return Ok();
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch(InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
