using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NetCore.Api.Helpers;
using ASP.NetCore.Models.DTOS;
using ASP.NetCore.Models.Models;
using ASP.NetCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASP.NetCore.Api.Controllers
{
    [Route("api/User")]
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



        [HttpPost("Login")]
        public IActionResult Login([FromBody] User user)
        {
            try
            {

                User result = _userService.Login(user);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                return Ok(new TokenDTO { token = JWTHelper.BuildToken(result, _config) });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpGet("Login")]
        [Authorize]
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
    }
}
