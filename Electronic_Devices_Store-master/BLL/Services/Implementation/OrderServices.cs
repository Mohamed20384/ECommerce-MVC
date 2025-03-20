using BLL.DTOs;
using BLL.Services.Absraction;
using DAL.Entities;
using DAL.Reposatory.Abstraction;
using DAL.Reposatory.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementation
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepo _orderRepo;

        public OrderServices(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task CreateNew(OrderDTO order)
        {
            var orderEntity = new Order
            {
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems.Select(item => new Product 
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    CategoryId = item.CategoryId,
                    ImageUrl = item.ImageUrl
                }).ToList()  
            };

            await _orderRepo.CreateNew(orderEntity);
        }

        public async Task Delete(int id)
        {
            await _orderRepo.Delete(id);
        }

        public async Task EditOrder(int id, OrderDTO newOrder)
        {
            var orderEntity = new Order
            {
                UserId = newOrder.UserId,
                OrderDate = newOrder.OrderDate,
                TotalAmount = newOrder.TotalAmount,
                OrderItems = newOrder.OrderItems.Select(item => new Product  
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    CategoryId = item.CategoryId,
                    ImageUrl = item.ImageUrl
                }).ToList()  
            };

            await _orderRepo.EditProduct(id, orderEntity);
        }

        public async Task<List<OrderDTO>> GetAll()
        {
            var listEntity = await _orderRepo.GetAll();
            var listDTO = listEntity.Select(o => new OrderDTO
            {
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                OrderItems = o.OrderItems.Select(item => new Product  
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    CategoryId = item.CategoryId,
                    ImageUrl = item.ImageUrl
                }).ToList()
            }).ToList();
            return listDTO;
        }

        public async Task<OrderDTO> GetbyId(int id)
        {
            var orderEntity = await _orderRepo.GetbyId(id);
            var orderDTO = new OrderDTO
            {
                UserId = orderEntity.UserId,
                OrderDate = orderEntity.OrderDate,
                TotalAmount = orderEntity.TotalAmount,
                OrderItems = orderEntity.OrderItems.Select(item => new Product
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    CategoryId = item.CategoryId,
                    ImageUrl = item.ImageUrl
                }).ToList()
            };
            return orderDTO;
        }
    }

}
