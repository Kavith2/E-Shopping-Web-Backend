using Backend1.Helper;
using Backend1.IService;
using Backend1.Models.Entities;
using Backend1.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend1.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Cart>> GetCart(string userId)
        {
            var cart = await _cartService.GetCartByUserId(userId);
            return Ok(cart);
        }


        [HttpPost("{add}")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            await _cartService.AddToCart(request.Id,request.UserId,request.ProductName,request.ProductPrice, request.ProductId, request.Quantity);
            return Ok(new { message = "Item added to Cart" });
        }


        [HttpPut("update/{productId}")]
        public async Task<IActionResult> UpdateQuantity(string productId, [FromBody] UpdateQuantityRequest request)
        {
            if (request.Quantity < 1)
                return BadRequest("Quantity must be at least 1.");

            var success = await _cartService.UpdateCartItemQuantity(request.userId, productId, request.Quantity);

            if (!success)
                return NotFound("Cart item not found.");

            return Ok(new { message = "Quantity updated successfully" });
        }

        [HttpPost("order")]
        public ActionResult AddCartToOrders([FromBody] Order order)
        {
            if (order == null || order.Items == null || !order.Items.Any())
                return BadRequest("Order or items cannot be null");

            _cartService.AddCartToOrders(order);
            return Ok(new { message = "Order placed successfully" });
        }




        [HttpDelete("{remove}")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartRequest request)
        {
            await _cartService.RemoveFromCart(request.UserId,request.ProductId);
            return Ok(new { message = "Item removed from Cart" });
        }

        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(string userId)
        {
            await _cartService.ClearCart(userId);
            return Ok(new { message = "Cart cleared" });
        }


    }
    

}
