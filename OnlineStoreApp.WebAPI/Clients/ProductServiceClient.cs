using OnlineStoreApp.Application.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace OnlineStoreApp.WebAPI.Clients
{
    public class ProductServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductServiceClient> _logger;

        public ProductServiceClient(HttpClient httpClient, ILogger<ProductServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> AddProduct(ProductDto dto)
        {
            _logger.LogInformation("Sending request to add product.");
            var response = await _httpClient.PostAsJsonAsync("/api/productservice/Product", dto);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error adding product: {response.StatusCode}");
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            _logger.LogInformation("Sending request to get products.");
            var response = await _httpClient.GetAsync("/api/productservice/Product");
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Successfully retrieved products.");
                return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
            }
            else
            {
                _logger.LogError($"Error retrieving products: {response.StatusCode}");
                return null;
            }
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            _logger.LogInformation($"Sending request to delete product with ID: {id}.");
            var response = await _httpClient.DeleteAsync($"/api/productservice/Product/{id}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error deleting product: {response.StatusCode}");
            }
            return response.IsSuccessStatusCode;
        }

        // Dodaj metodę GetProductByIdAsync
        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            _logger.LogInformation($"Sending request to get product with ID: {id}.");
            var response = await _httpClient.GetAsync($"/api/productservice/Product/{id}");
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Successfully retrieved product with ID: {id}.");
                return await response.Content.ReadFromJsonAsync<ProductDto>();
            }
            else
            {
                _logger.LogError($"Error retrieving product with ID: {id}: {response.StatusCode}");
                return null;
            }
        }

        public async Task<bool> UpdateProductAsync(ProductDto dto, string token)
        {
            _logger.LogInformation($"Sending request to update product with ID: {dto.Id}.");
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/productservice/Product/{dto.Id}")
            {
                Content = JsonContent.Create(dto)
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error updating product: {response.StatusCode}");
            }
            return response.IsSuccessStatusCode;
        }

    }
}