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
            return Ok(_userService.UpdateUser(id, userDto));
        }
    }
}
