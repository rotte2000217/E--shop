using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;
using WebshopServer.Exceptions;
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
            OrderDto order;

            try
            {
                order = _orderService.GetOrderById(id);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok(order);
        }

        [HttpPost]
        [Authorize(Roles = "Buyer")]
        public IActionResult CreateOrder([FromBody] OrderDto orderDto)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            OrderDto order;

            try
            {
                order = _orderService.CreateOrder(orderDto, userId);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidFieldsException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(order);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Buyer")]
        public IActionResult DeleteOrder(long id)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            try
            {
                _orderService.CancelOrder(id, userId);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ForbiddenActionException)
            {
                return Forbid();
            }

            return NoContent();
        }
    }
}
