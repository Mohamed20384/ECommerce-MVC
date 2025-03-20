using DAL.Database;
using DAL.Entities;
using DAL.Reposatory.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Reposatory.Implementation
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ApplicationDbContext _context;

        public OrderRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetbyId(int id)
        {
            return await _context.Orders.Include(o => o.User)
                                        .Include(o => o.OrderItems)
                                        .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders.Include(o => o.User)
                                        .Include(o => o.OrderItems)
                                        .ToListAsync();
        }

        public async Task CreateNew(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task EditProduct(int id, Order newOrder)
        {
            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder != null)
            {
                existingOrder.UserId = newOrder.UserId;
                existingOrder.OrderDate = newOrder.OrderDate;
                existingOrder.TotalAmount = newOrder.TotalAmount;
                existingOrder.OrderItems = newOrder.OrderItems;

                _context.Orders.Update(existingOrder);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
