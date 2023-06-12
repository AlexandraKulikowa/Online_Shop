using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task AddAsync(Order order);
        Task<Order> GetOrderAsync(int id);
        Task ChangeStatusAsync(int id, Status status);
    }
}