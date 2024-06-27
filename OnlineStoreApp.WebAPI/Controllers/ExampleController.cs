using Microsoft.AspNetCore.Mvc;
using OnlineStoreApp.WebAPI.Services;
using System.Threading.Tasks;

namespace OnlineStoreApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleController : ControllerBase
    {
        private readonly HttpClientService _httpClientService;

        public ExampleController(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        [HttpGet("protecteddata")]
        public async Task<IActionResult> GetProtectedData()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var data = await _httpClientService.GetProtectedDataAsync(token);
            if (data != null)
            {
                return Ok(data);
            }
            return Unauthorized();
        }
    }
}