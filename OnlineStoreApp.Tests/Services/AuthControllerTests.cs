using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GenerateToken_ReturnsToken()
    {
        var response = await _client.PostAsync("/api/auth/token", null);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("token", content);
    }
}
