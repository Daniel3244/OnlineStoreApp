using System;
using System.Threading.Tasks;
using OnlineStoreApp.Application.DTOs;
using OnlineStoreApp.Domain.Entities;
using OnlineStoreApp.Domain.Interfaces;

namespace OnlineStoreApp.UserService.Services
{
    public class UserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserServices(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task RegisterUserAsync(UserRegistrationDto dto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                PasswordHash = _passwordHasher.HashPassword(dto.Password),
                Role = "User"
            };

            await _userRepository.AddAsync(user);
        }

        public async Task<User> LoginAsync(UserLoginDto dto)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username);
            if (user == null || !_passwordHasher.HashPassword(dto.Password).Equals(user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return user;
        }
    }
}
