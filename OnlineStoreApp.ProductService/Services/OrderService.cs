using OnlineStoreApp.Application.DTOs;
using OnlineStoreApp.Domain.Entities;
using OnlineStoreApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreApp.ProductService.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ProductServices _productServices;

        public OrderService(IOrderRepository orderRepository, ProductServices productServices)
        {
            _orderRepository = orderRepository;
            _productServices = productServices;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(Guid userId)
        {
            var orders = await _orderRepository.GetByUserIdAsync(userId);
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            });
        }

        public async Task AddOrderAsync(OrderDto dto)
        {
            foreach (var item in dto.OrderItems)
            {
                var success = await _productServices.UpdateProductStockAsync(item.ProductId, -item.Quantity);
                if (!success)
                {
                    throw new InvalidOperationException("Insufficient stock for product " + item.ProductId);
                }
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = dto.TotalPrice,
                OrderItems = dto.OrderItems.Select(oi => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };

            await _orderRepository.AddAsync(order);
        }
    }
}