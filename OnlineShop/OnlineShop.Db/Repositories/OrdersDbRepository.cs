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
            return databaseContext.Orders.ToList();
        }

        public void Add(Order order, string userId)
        {
            var existingOrder = GetOrder(order.Id);

            if (existingOrder == null)
            {
                var newOrder = new Order
                {
                    UserId = userId,
                    Contacts = order.Contacts,
                    TimeFromTo = order.TimeFromTo,
                    Email = order.Email,
                    Mailto = order.Mailto,
                    DateofDelivery = order.DateofDelivery,
                    Comment = order.Comment,
                    Packaging = order.Packaging,
                    isAccept = order.isAccept,
                    Status = order.Status,
                    DateofOrder = order.DateofOrder
                };

                newOrder.Products = order.Products.ToList();
                foreach (var product in newOrder.Products)
                {
                    product.Order = newOrder;
                }
                databaseContext.Orders.Add(newOrder);
                databaseContext.SaveChanges();
            }
        }
        public Order GetOrder(int id)
        {
            var order = databaseContext.Orders.Include(x => x.Products).FirstOrDefault(x => x.Id == id);
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