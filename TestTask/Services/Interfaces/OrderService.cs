using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Services.Interfaces
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order> GetOrder()
        {
            var orders = await _context.Orders
                .ToListAsync();

            var order = await Task.Run(() =>
            {
                int maxOrderPrice = orders.Max(o => o.Price * o.Quantity);

                return orders.FirstOrDefault(o => o.Price * o.Quantity == maxOrderPrice);
            });

            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.Where(o => o.Quantity > 10).ToListAsync();
        }
    }
}
