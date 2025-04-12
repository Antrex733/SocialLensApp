using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using SocialLensApp.Models;
using SocialLensApp.Services.Interfaces;

namespace SocialLensApp.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public IActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            var id = _userService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult LogInUser([FromBody] LogInUserDto dto)
        {
            var token = _userService.LogInUser(dto);
            return Ok(token);
        }

        [HttpDelete("delete")]
        
        public IActionResult deleteAccount()
        {
            _userService.DeleteAccount();
            return Ok();
        }

        [HttpPatch("follow/{id}")]
        public IActionResult followAccount([FromRoute] int id)
        {
            _userService.FollowAccount(id);
            return Ok();
        }

        [HttpDelete("unfollow/{id}")]
        public IActionResult unfollow([FromRoute] int id)
        {
            _userService.Unfollow(id);
            return Ok();
        }

        [HttpPost("sendfriendrequest/{id}")]
        public async Task<IActionResult> sendFriendRequest([FromRoute] int id)
        {
            await _userService.SendFriendRequest(id);
            return Ok();
        }

        [HttpGet("showfriendrequests")]
        public IActionResult showFriendRequests()
        {
            var inviteList = _userService.showFriendRequests();
            return Ok(inviteList);
        }

        [HttpDelete("denyfriendrequest/{id}")]
        public IActionResult denyFriendRequest([FromRoute] int id)
        {
            _userService.DenyFriendRequest(id);
            return Ok();
        }

        [HttpPatch("acceptfriendrequest/{id}")]
        public IActionResult acceptFriendRequest([FromRoute] int id)
        {
            _userService.AcceptFriendRequest(id);
            return Ok();
        }

        [HttpDelete("unfriend/{id}")]
        public IActionResult unfriend([FromRoute] int id)
        {
            _userService.RemoveFriend(id);
            return Ok();
        }

        [HttpPatch("block/{id}")]
        public IActionResult block([FromRoute] int id)
        {
            _userService.Block(id);
            return Ok();
        }

        [HttpGet("showblockedlist")]
        public IActionResult showBlockedList()
        {
            var blocked = _userService.ShowBLockedList();
            return Ok(blocked);
        }

        [HttpDelete("unblock/{id}")]
        public IActionResult unblock([FromRoute]int id)
        {
            _userService.Unblock(id);
            return Ok();
        }

        [HttpGet("searchuser")]
        public IActionResult searchUser([FromHeader] string search)
        {
            var Users = _userService.SearchUser(search);
            return Ok(Users);
        }
       
    }
}
