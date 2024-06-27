using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApp.Application.DTOs;
using OnlineStoreApp.WebAPI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace OnlineStoreApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/webapi/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly HttpClientService _httpClientService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(HttpClientService httpClientService, ILogger<ProductController> logger)
        {
            _httpClientService = httpClientService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddProduct(ProductDto dto)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _logger.LogInformation("Attempting to add product through HttpClientService.");
            var result = await _httpClientService.AddProductAsync(dto, token);
            if (result)
            {
                _logger.LogInformation("Product added successfully.");
                return Ok();
            }
            _logger.LogError("Failed to add product.");
            return BadRequest("Failed to add product.");
        }

        [HttpDelete("{id}")]
        [Authorize]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _logger.LogInformation($"Attempting to delete product with ID: {id} through HttpClientService.");
            var result = await _httpClientService.DeleteProductAsync(id, token);
            if (result)
            {
                _logger.LogInformation($"Product with ID: {id} deleted successfully.");
                return Ok();
            }
            _logger.LogError($"Failed to delete product with ID: {id}.");
            return NotFound($"Product with ID: {id} not found.");
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _logger.LogInformation("Attempting to retrieve products through HttpClientService.");
            var products = await _httpClientService.GetProductsAsync(token);
            if (products != null)
            {
                _logger.LogInformation("Products retrieved successfully.");
                return Ok(products);
            }
            _logger.LogError("Failed to retrieve products.");
            return BadRequest("Failed to retrieve products.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _logger.LogInformation($"Attempting to retrieve product with ID: {id} through HttpClientService.");
            var product = await _httpClientService.GetProductByIdAsync(id, token);
            if (product != null)
            {
                _logger.LogInformation($"Product with ID: {id} retrieved successfully.");
                return Ok(product);
            }
            _logger.LogError($"Failed to retrieve product with ID: {id}.");
            return NotFound($"Product with ID: {id} not found.");
        }

        [HttpPut("{id}")]
        [Authorize]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductDto dto)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (id != dto.Id)
            {
                return BadRequest("Product ID mismatch");
            }

            _logger.LogInformation($"Attempting to update product with ID: {dto.Id} through HttpClientService.");
            var result = await _httpClientService.UpdateProductAsync(dto, token);
            if (result)
            {
                _logger.LogInformation($"Product with ID: {dto.Id} updated successfully.");
                return Ok();
            }
            _logger.LogError($"Failed to update product with ID: {dto.Id}.");
            return NotFound($"Product with ID: {dto.Id} not found.");
        }
    }
}