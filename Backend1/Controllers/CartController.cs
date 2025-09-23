using Backend1.Helper;
using Backend1.IService;
using Backend1.Models.Entities;
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
