using OnlineShopWebApp.Interfaces;
using System.Collections.Generic;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Repositories
{
    public class InMemoryOrdersRepository : IOrderRepository
    {
        private List<Order> orders = new List<Order>();
        public List<Order> Orders
        {
            get
            {
                return orders;
            }
            set
            {
                orders = value;
            }
        }
        public void Add(Order order)
        {
            orders.Add(order);
        }
    }
}