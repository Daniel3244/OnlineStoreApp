using OnlineStoreApp.Domain.Entities;
using OnlineStoreApp.Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace OnlineStoreApp.Tests.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(AppDbContext db)
        {
            db.Users.AddRange(GetSeedingUsers());
            db.Products.AddRange(GetSeedingProducts());
            db.Orders.AddRange(GetSeedingOrders());
            db.SaveChanges();
        }

        public static List<User> GetSeedingUsers()
        {
            return new List<User>
            {
                new User { Username = "testuser", PasswordHash = "password123" },
            };
        }

        public static List<Product> GetSeedingProducts()
        {
            return new List<Product>
            {
                new Product { Name = "Test Product 1", Description = "Description 1", Price = 10.0m },
                new Product { Name = "Test Product 2", Description = "Description 2", Price = 20.0m },
            };
        }

        public static List<Order> GetSeedingOrders()
        {
            return new List<Order>
            {
                new Order { UserId = Guid.NewGuid(), OrderDate = DateTime.UtcNow },
            };
        }
    }
}
