using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface ICompareRepository
    {
        Task<Comparison> TryGetByIdAsync(string userId, int id);
        Task<List<Product>> GetAllAsync(string userId);
        Task AddAsync(Product product, string userId);
        Task DeleteProductAsync(string userId, int id);
        Task ClearAsync(string userId);
    }
}