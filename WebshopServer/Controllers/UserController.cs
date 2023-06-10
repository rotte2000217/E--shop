using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;
using WebshopServer.Exceptions;
using WebshopServer.Interfaces;
using WebshopServer.QueryParameters;

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
            UserResponseDto user;

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
        public IActionResult RegisterUser([FromBody] RegisterRequestDto requestDto)
        {
            UserResponseDto user;

            try
            {
                user = _userService.RegisterUser(requestDto);
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
        public IActionResult UpdateUser(long id, [FromBody] UserRequestDto requestDto)
        {
            if (!User.HasClaim("Id", id.ToString()))
            {
                return Forbid();
            }

            UserResponseDto user;

            try
            {
                user = _userService.UpdateUser(id, requestDto);
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
        public IActionResult LoginUser([FromBody] LoginRequestDto requestDto)
        {
            LoginResponseDto responseDto;

            try
            {
                responseDto = _userService.LoginUser(requestDto);
            }
            catch (InvalidCredentialsException e)
            {
                return Unauthorized(e.Message);
            }
            catch (InvalidFieldsException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(responseDto);
        }

        [HttpPost("verify/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult VerifyUser(long id, [FromBody] VerificationRequestDto requestDto)
        {
            VerificationResponseDto user;

            try
            {
                user = _userService.VerifyUser(id, requestDto);
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
    }
}
