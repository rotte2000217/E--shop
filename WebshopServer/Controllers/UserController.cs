using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;
using WebshopServer.Interfaces;

namespace WebshopServer.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(long id)
        {
            if (!User.HasClaim("Id", id.ToString()))
            {
                return Unauthorized();
            }

            return Ok(_userService.GetUserById(id));
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] UserDto userDto)
        {
            return Ok(_userService.RegisterUser(userDto));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(long id, [FromBody] UserDto userDto)
        {
            if (!User.HasClaim("Id", id.ToString()))
            {
                return Unauthorized();
            }

            return Ok(_userService.UpdateUser(id, userDto));
        }

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginDto loginDto)
        {
            string token = _userService.LoginUser(loginDto);
            
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
