using AutoMapper;
using Core.Entities;
using Core.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.DTOs;

namespace Web.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Cart>> GetCart(string id)
        {
            var cart = await _cartRepository.GetCartAsync(id);
            return Ok(cart ?? new Cart(id));
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateOrUpdateCart(CartDTO cartDTO)
        {
            var cart = _mapper.Map<CartDTO, Cart>(cartDTO);
            var updatedCart = await _cartRepository.CreateOrUpdateCartAsync(cart);
            return Ok(updatedCart);
        }

        [HttpDelete]
        public async Task DeleteCart(string id)
        {
            await _cartRepository.DeleteCartAsync(id);
        }
    }
}
