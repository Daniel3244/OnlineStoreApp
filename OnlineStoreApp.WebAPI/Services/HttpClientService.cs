using OnlineStoreApp.Application.DTOs;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OnlineStoreApp.WebAPI.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _client;

        public HttpClientService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetProtectedDataAsync(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync("http://localhost:5000/api/protectedendpoint");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task<string> GetProductsAsync(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync("http://localhost:5166/api/productservice/Product");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task<string> GetProductByIdAsync(Guid id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"http://localhost:5166/api/productservice/Product/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task<bool> AddProductAsync(ProductDto product, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsJsonAsync("http://localhost:5166/api/productservice/Product", product);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductAsync(Guid id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"http://localhost:5166/api/productservice/Product/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProductAsync(ProductDto product, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PutAsJsonAsync($"http://localhost:5166/api/productservice/Product/{product.Id}", product);
            return response.IsSuccessStatusCode;
        }
    }
}