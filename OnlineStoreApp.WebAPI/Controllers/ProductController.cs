using Microsoft.AspNetCore.Mvc;
using OnlineStoreApp.Application.DTOs;
using OnlineStoreApp.WebAPI.Clients;
using System.Threading.Tasks;

namespace OnlineStoreApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/webapi/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductServiceClient _productServiceClient;

        public ProductController(ProductServiceClient productServiceClient)
        {
            _productServiceClient = productServiceClient;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto dto)
        {
            var result = await _productServiceClient.AddProduct(dto);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Failed to add product.");
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productServiceClient.GetProducts();
            return Ok(products);
        }
    }
}
