using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using OnlineStoreApp.Application.DTOs;
using OnlineStoreApp.Domain.Entities;
using OnlineStoreApp.Domain.Interfaces;
using OnlineStoreApp.ProductService.Services;
using Xunit;

namespace OnlineStoreApp.Tests.Services
{
    public class ProductServicesTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductServices _productServices;

        public ProductServicesTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _productServices = new ProductServices(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1", Description = "Description 1", Price = 10, Stock = 100 },
                new Product { Id = Guid.NewGuid(), Name = "Product 2", Description = "Description 2", Price = 20, Stock = 200 },
            };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _productServices.GetAllProductsAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Product 1", result.First().Name);
        }

        [Fact]
        public async Task AddProductAsync_AddsProduct()
        {
            // Arrange
            var productDto = new ProductDto
            {
                Name = "New Product",
                Description = "New Description",
                Price = 30,
                Stock = 300
            };

            // Act
            await _productServices.AddProductAsync(productDto);

            // Assert
            _mockRepo.Verify(repo => repo.AddAsync(It.Is<Product>(p => p.Name == productDto.Name)), Times.Once);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                Name = "Product",
                Description = "Description",
                Price = 40,
                Stock = 400
            };
            _mockRepo.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _productServices.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsNull_WhenProductNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await _productServices.GetProductByIdAsync(productId);

            // Assert
            Assert.Null(result);
        }
    }
}
