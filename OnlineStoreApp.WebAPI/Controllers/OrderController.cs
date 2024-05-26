using Microsoft.AspNetCore.Mvc;
using OnlineStoreApp.Application.DTOs;
using OnlineStoreApp.Application.Services;
using System.Threading.Tasks;

namespace OnlineStoreApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Add(OrderDto dto)
        {
            await _orderService.AddOrderAsync(dto);
            return Ok();
        }
    }
}
