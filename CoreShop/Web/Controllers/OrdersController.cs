using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.DTOs;
using Web.Errors;
using Web.Extensions;

namespace Web.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(
            IOrderService orderService,
            IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {
            var email = HttpContext.User.GetEmailFromPrincipal();
            var shippingAddress = _mapper.Map<AddressDTO, Address>(orderDTO.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(email, orderDTO.DeliveryMethodId, orderDTO.CartId, shippingAddress);
            
            if (order is null)
            {
                return BadRequest(new ResponseBody(400, "The order cannot be created"));
            }

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrders()
        {
            var email = HttpContext.User.GetEmailFromPrincipal();
            var orders = await _orderService.GetOrdersAsync(email);
            var response = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDTO>>(orders);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrder(int id)
        {
            var email = HttpContext.User.GetEmailFromPrincipal();
            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order is null)
            {
                return NotFound(new ResponseBody(404));
            }

            var response = _mapper.Map<Order, OrderToReturnDTO>(order);
            return response;
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}
