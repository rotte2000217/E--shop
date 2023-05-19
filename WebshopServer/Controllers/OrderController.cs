using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;
using WebshopServer.Interfaces;

namespace WebshopServer.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(long id)
        {
            return Ok(_orderService.GetOrderById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Buyer")]
        public IActionResult CreateOrder([FromBody] OrderDto orderDto)
        {
            return Ok(_orderService.CreateOrder(orderDto));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Buyer")]
        public IActionResult DeleteOrder(long id)
        {
            _orderService.CancelOrder(id);

            return NoContent();
        }
    }
}
