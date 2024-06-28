﻿using OnlineStoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task DeleteAsync(Product product);
        Task UpdateAsync(Product product);
    }
}