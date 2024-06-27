using Microsoft.AspNetCore.Mvc;
using OnlineStoreApp.Application.DTOs;
using OnlineStoreApp.WebAPI.Clients;
using System.Threading.Tasks;

namespace OnlineStoreApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/webapi/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserServiceClient _userServiceClient;

        public UserController(UserServiceClient userServiceClient)
        {
            _userServiceClient = userServiceClient;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto dto)
        {
            var result = await _userServiceClient.RegisterUser(dto);
            if (result)
            {
                return Ok();
            }
            return BadRequest("User registration failed.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var loginResponse = await _userServiceClient.LoginUser(dto);
            if (loginResponse != null)
            {
                return Ok(new { Token = loginResponse.Token, Role = loginResponse.Role });
            }
            return Unauthorized();
        }
    }
}
