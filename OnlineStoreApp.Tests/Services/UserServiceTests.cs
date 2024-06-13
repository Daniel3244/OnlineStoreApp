using System;
using System.Threading.Tasks;
using Moq;
using OnlineStoreApp.Application.DTOs;
using OnlineStoreApp.Domain.Entities;
using OnlineStoreApp.Domain.Interfaces;
using OnlineStoreApp.UserService.Services;
using Xunit;

namespace OnlineStoreApp.Tests.Services
{
    public class UserServicesTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly Mock<IPasswordHasher> _mockHasher;
        private readonly UserServices _userServices;

        public UserServicesTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _mockHasher = new Mock<IPasswordHasher>();
            _userServices = new UserServices(_mockRepo.Object, _mockHasher.Object);
        }

        [Fact]
        public async Task RegisterUserAsync_AddsUser()
        {
            // Arrange
            var userDto = new UserRegistrationDto
            {
                Username = "testuser",
                Password = "password123"
            };
            _mockHasher.Setup(h => h.HashPassword(userDto.Password)).Returns("hashedPassword");

            // Act
            await _userServices.RegisterUserAsync(userDto);

            // Assert
            _mockRepo.Verify(repo => repo.AddAsync(It.Is<User>(u => u.Username == userDto.Username && u.PasswordHash == "hashedPassword" && u.Role == "User")), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ReturnsUser_WhenCredentialsAreValid()
        {
            // Arrange
            var userDto = new UserLoginDto
            {
                Username = "testuser",
                Password = "password123"
            };
            var hashedPassword = "hashedPassword";
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = userDto.Username,
                PasswordHash = hashedPassword,
                Role = "User"
            };

            _mockRepo.Setup(repo => repo.GetByUsernameAsync(userDto.Username)).ReturnsAsync(user);
            _mockHasher.Setup(h => h.HashPassword(userDto.Password)).Returns(hashedPassword);

            // Act
            var result = await _userServices.LoginAsync(userDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userDto.Username, result.Username);
        }

        [Fact]
        public async Task LoginAsync_ThrowsUnauthorizedAccessException_WhenCredentialsAreInvalid()
        {
            // Arrange
            var userDto = new UserLoginDto
            {
                Username = "testuser",
                Password = "wrongpassword"
            };
            var hashedPassword = "hashedPassword";
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = userDto.Username,
                PasswordHash = hashedPassword,
                Role = "User"
            };

            _mockRepo.Setup(repo => repo.GetByUsernameAsync(userDto.Username)).ReturnsAsync(user);
            _mockHasher.Setup(h => h.HashPassword(userDto.Password)).Returns("wrongHashedPassword");

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _userServices.LoginAsync(userDto));
        }

        [Fact]
        public async Task LoginAsync_ThrowsUnauthorizedAccessException_WhenUserDoesNotExist()
        {
            // Arrange
            var userDto = new UserLoginDto
            {
                Username = "nonexistentuser",
                Password = "password123"
            };

            _mockRepo.Setup(repo => repo.GetByUsernameAsync(userDto.Username)).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _userServices.LoginAsync(userDto));
        }
    }
}
