using System.Collections.Generic;
using System.Linq;
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

        public List<Order> GetAll()
        {
            return databaseContext.Orders
                .Include(x => x.Contacts)
                .Include(x => x.OrderBasketItems)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Size)
                .ToList();
        }

        public void Add(Order order)
        {
                databaseContext.Orders.Add(order);
                databaseContext.SaveChanges();
        }
        public Order GetOrder(int id)
        {
            var order = databaseContext.Orders
                .Include(x => x.Contacts)
                .Include(x => x.OrderBasketItems)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Size)
                .FirstOrDefault(x => x.Id == id);
            return order;
        }

        public void ChangeStatus(int id, Status status)
        {
            var existingOrder = GetOrder(id);
            existingOrder.Status = status;
            databaseContext.SaveChanges();
        }
    }
}