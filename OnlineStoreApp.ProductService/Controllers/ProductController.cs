using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApp.Application.DTOs;
using OnlineStoreApp.ProductService.Services;
using System.Threading.Tasks;

namespace OnlineStoreApp.ProductService.Controllers
{
    [ApiController]
    [Route("api/productservice/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ProductServices _productService;

        public ProductController(ProductServices productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto dto)
        {
            await _productService.AddProductAsync(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
