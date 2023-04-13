using System.Collections.Generic;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> Orders { get; set; }
        void Add(Order order);
        Order GetOrder(int id);
    }
}