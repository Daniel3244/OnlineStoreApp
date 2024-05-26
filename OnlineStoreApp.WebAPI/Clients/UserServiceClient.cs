using OnlineStoreApp.Application.DTOs;

public class LoginResponse
{
    public string Token { get; set; }
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

    public async Task<string> LoginUser(UserLoginDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/userservice/User/login", dto);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result?.Token;
        }
        return null;
    }
}
