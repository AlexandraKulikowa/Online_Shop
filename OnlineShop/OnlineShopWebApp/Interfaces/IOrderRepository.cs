using System.Collections.Generic;
using OnlineShopWebApp.Areas.Admin.Models;

namespace OnlineShopWebApp.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        void Add(Order order);
        Order GetOrder(int id);
        void ChangeStatus(int id, Status status);
    }
}