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
            UserDto user;

            try
            {
                user = _userService.GetUserById(id);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] UserDto userDto)
        {
            UserDto user;

            try
            {
                user = _userService.RegisterUser(userDto);
            }
            catch (InvalidCredentialsException e)
            {
                return Conflict(e.Message);
            }
            catch (InvalidFieldsException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(long id, [FromBody] UserDto userDto)
        {
            if (!User.HasClaim("Id", id.ToString()))
            {
                return Forbid();
            }

            UserDto user;

            try
            {
                user = _userService.UpdateUser(id, userDto);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidFieldsException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginDto loginDto)
        {
            string token;

            try
            {
                token = _userService.LoginUser(loginDto);
            }
            catch (InvalidCredentialsException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidFieldsException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(token);
        }

        [HttpPost("verify")]
        [Authorize(Roles = "Admin")]
        public IActionResult VerifyUser([FromBody] VerifyDto verifyDto)
        {
            UserDto user;

            try
            {
                user = _userService.VerifyUser(verifyDto);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok(user);
        }
    }
}
