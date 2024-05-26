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

        public async Task<string> GetProtectedDataAsync()
        {
            var token = await GetJwtTokenAsync(); // Uzyskaj token z serwisu uwierzytelniającego
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync("http://localhost:5000/api/protectedendpoint");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        private async Task<string> GetJwtTokenAsync()
        {
            // Przykład uzyskania tokenu JWT z serwisu uwierzytelniającego
            var response = await _client.PostAsync("http://localhost:5091/api/auth/token", null);
            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadAsStringAsync();
                // Wyodrębnij token z odpowiedzi (zależy od formatu odpowiedzi serwisu uwierzytelniającego)
                return tokenResponse; // Zastąp to odpowiednim kodem wyodrębniania tokenu
            }

            return null;
        }
    }
}
