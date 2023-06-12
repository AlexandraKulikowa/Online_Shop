using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repositories
{
    public class OrdersDbRepository : IOrderRepository
    {
        private readonly DatabaseContext databaseContext;

        public OrdersDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await databaseContext.Orders
                .Include(x => x.Contacts)
                .Include(x => x.OrderBasketItems)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Size)
                .ToListAsync();
        }

        public async Task AddAsync(Order order)
        {
                await databaseContext.Orders.AddAsync(order);
                await databaseContext.SaveChangesAsync();
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            var order = await databaseContext.Orders
                .Include(x => x.Contacts)
                .Include(x => x.OrderBasketItems)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Size)
                .FirstOrDefaultAsync(x => x.Id == id);
            return order;
        }

        public async Task ChangeStatusAsync(int id, Status status)
        {
            var existingOrder = await GetOrderAsync(id);
            existingOrder.Status = status;
            await databaseContext.SaveChangesAsync();
        }
    }
}