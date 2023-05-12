using OnlineShop.Db.Models;
using System.Collections.Generic;

namespace OnlineShop.Db.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        void Add(Order order, string userId);
        Order GetOrder(int id);
        void ChangeStatus(int id, Status status);
    }
}