using OnlineStoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreApp.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId);
        Task AddAsync(Order order);
    }
}