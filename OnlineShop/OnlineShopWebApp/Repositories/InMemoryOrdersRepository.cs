using OnlineShopWebApp.Interfaces;
using System.Collections.Generic;

namespace OnlineShopWebApp.Repositories
{
    public class InMemoryOrdersRepository : IOrderRepository
    {
        private List<Basket> orders = new List<Basket>();
        public List<Basket> Orders
        {
            get
            {
                return orders;
            }
        }
        public void Add(Basket basket)
        {
            orders.Add(basket);
        }
    }
}
