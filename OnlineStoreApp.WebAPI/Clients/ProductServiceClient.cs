using OnlineStoreApp.Application.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OnlineStoreApp.WebAPI.Clients
{
    public class ProductServiceClient
    {
        private readonly HttpClient _httpClient;

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddProduct(ProductDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/productservice/Product", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var response = await _httpClient.GetAsync("/api/productservice/Product");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
            }
            return null;
        }
    }
}
