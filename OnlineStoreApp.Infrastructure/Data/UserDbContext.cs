using Microsoft.EntityFrameworkCore;
using OnlineStoreApp.Domain.Entities;

namespace OnlineStoreApp.Infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}