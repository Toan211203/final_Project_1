using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Cart;
using api.Repositories.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            var carts = await _cartRepository.GetAllCartsAsync();
            return Ok(carts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartById([FromRoute] int id)
        {
            var cart = await _cartRepository.GetCartByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CreateCartDTO createCartDTO)
        {
            // var userIdClaim = User.FindFirst("UserId");
            // if (userIdClaim == null)
            // {
            //     return Unauthorized("User not authenticated.");
            // }
            //var userId = int.Parse(userIdClaim.Value);
            //createCartDTO.UserId = userId;
            var cart = await _cartRepository.AddCartAsync(createCartDTO);
            return CreatedAtAction(nameof(GetCartById), new { id = cart?.CartId }, cart);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] UpdateCartDTO updateCartDTO)
        {
            var cart = await _cartRepository.UpdateCartAsync(id, updateCartDTO);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _cartRepository.DeleteCartAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCartByUserId([FromRoute] int userId)
        {
            var cartItems = await _cartRepository.GetCartByUserIdAsync(userId);
            return Ok(cartItems);
        }

        // DELETE: api/cart/user/{userId}
        [HttpDelete("user/{userId}")]
        public async Task<IActionResult> DeleteCartByUserIdAsync(int userId)
        {
            var cartsDeleted = await _cartRepository.DeleteCartByUserIdAsync(userId);

            if (!cartsDeleted.Any())
            {
                return NotFound("No carts found for this user.");
            }
            return Ok(cartsDeleted);
        }
        
    }
}