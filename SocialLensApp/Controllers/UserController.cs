using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using SocialLensApp.Models;
using SocialLensApp.Services.Interfaces;

namespace SocialLensApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody]RegisterUserDto dto)
        {
            _userService.RegisterUser(dto);
            return Ok();
        }
        [HttpPost("login")]
        public IActionResult LogInUser([FromBody] LogInUserDto dto)
        {
            var token =_userService.LogInUser(dto);
            return Ok(token);
        }

    }
}
