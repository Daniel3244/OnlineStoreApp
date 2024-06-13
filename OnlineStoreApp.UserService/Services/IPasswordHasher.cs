namespace OnlineStoreApp.UserService.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}