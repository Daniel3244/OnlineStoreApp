using OnlineStoreApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStoreApp.Domain.Interfaces;
using OnlineStoreApp.Domain.Entities;


namespace OnlineStoreApp.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(Guid userId)
        {
            var orders = await _orderRepository.GetByUserIdAsync(userId);
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
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
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                OrderDate = dto.OrderDate,
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

