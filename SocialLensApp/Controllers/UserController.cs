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

    }
}
