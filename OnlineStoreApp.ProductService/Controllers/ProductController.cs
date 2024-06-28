using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApp.Application.DTOs;
using OnlineStoreApp.ProductService.Services;
using System.Threading.Tasks;
using System;

namespace OnlineStoreApp.ProductService.Controllers
{
    [ApiController]
    [Route("api/productservice/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductServices _productService;

        public ProductController(ProductServices productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize]
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

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Product ID mismatch");
            }

            var result = await _productService.UpdateProductAsync(dto);
            if (result)
            {
                return Ok();
            }
            return NotFound($"Product with ID: {dto.Id} not found.");
        }
    }
}