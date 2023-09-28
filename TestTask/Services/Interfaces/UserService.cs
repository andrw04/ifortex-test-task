using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;

namespace TestTask.Services.Interfaces
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUser()
        {
            var users = await _context.Users.
                Include(u => u.Orders).
                ToListAsync();

            var user = await Task.Run(() =>
            {
                int maxOrders = users.Max(u => u.Orders.Count);

                return users.FirstOrDefault(u => u.Orders.Count == maxOrders);
            });

            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.Where(u => u.Status == UserStatus.Inactive).Include(u => u.Orders).ToListAsync();
        }
    }
}
