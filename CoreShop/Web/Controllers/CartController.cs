using Core.Entities;
using Core.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Cart>> GetCart(string id)
        {
            var cart = await _cartRepository.GetCartAsync(id);
            return Ok(cart ?? new Cart(id));
        }

        public async Task<ActionResult<Cart>> CreateOrUpdateCart(Cart other)
        {
            var cart = await _cartRepository.CreateOrUpdateCartAsync(other);
            return Ok(cart);
        }

        [HttpDelete]
        public async Task DeleteCart(string id)
        {
            await _cartRepository.DeleteCartAsync(id);
        }
    }
}
