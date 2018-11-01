using ASP.NetCore.Api.Helpers;
using ASP.NetCore.Models.DTOS;
using ASP.NetCore.Models.Models;
using ASP.NetCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace ASP.NetCore.Api.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private IConfiguration _config;

        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] User user)
        {
            try
            {

                User result = _userService.Login(user);
                if (result == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                return Ok(new TokenDTO { token = JWTHelper.BuildToken(result, _config) });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            try
            {
                return Ok(_userService.Users());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody] User user)
        {
            try
            {

                string result = _userService.AddUsers(user);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error");
                }
                return Ok(new { message = result });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(long Id)
        {
            try
            {

                string result = _userService.DeleteUser(Id);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error");
                }
                return Ok(new { message = result });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
    }
}
