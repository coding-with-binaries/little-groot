using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LittleGrootServer.Dto;
using LittleGrootServer.Services;

namespace LittleGrootServer.Controllers {
    [Authorize]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CartController : ControllerBase {
        private ICartService _cartService = null;

        public CartController(ICartService cartService) {
            _cartService = cartService;
        }

        [HttpGet("items")]
        public ActionResult<CartDto> GetUserCart() {
            var cartDto = _cartService.GetUserCart();
            if (cartDto == null) {
                return NotFound();
            }
            return Ok(cartDto);
        }
    }
}