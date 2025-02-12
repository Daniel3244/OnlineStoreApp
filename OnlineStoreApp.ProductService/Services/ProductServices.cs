﻿using OnlineStoreApp.Application.DTOs;
using OnlineStoreApp.Domain.Entities;
using OnlineStoreApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreApp.ProductService.Services
{
    public class ProductServices
    {
        private readonly IProductRepository _productRepository;

        public ProductServices(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock
            });
        }

        public async Task AddProductAsync(ProductDto dto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock
            };

            await _productRepository.AddAsync(product);
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }
            await _productRepository.DeleteAsync(product);
            return true;
        }

        public async Task<bool> UpdateProductAsync(ProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(dto.Id);
            if (product == null)
            {
                return false;
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;

            await _productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<bool> UpdateProductStockAsync(Guid productId, int quantityChange)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return false;
            }

            if (product.Stock + quantityChange < 0)
            {
                return false;
            }

            product.Stock += quantityChange;
            await _productRepository.UpdateAsync(product);
            return true;
        }
    }
}