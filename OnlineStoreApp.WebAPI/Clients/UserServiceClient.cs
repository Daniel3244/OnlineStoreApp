using OnlineStoreApp.Application.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class LoginResponse
{
    public string Token { get; set; }
    public string Role { get; set; } // Ensure role is included in the response
}

public class UserServiceClient
{
    private readonly HttpClient _httpClient;

    public UserServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> RegisterUser(UserRegistrationDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/userservice/User/register", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<LoginResponse> LoginUser(UserLoginDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/userservice/User/login", dto);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result; // Return the entire response including the token and role
        }
        return null;
    }
}
