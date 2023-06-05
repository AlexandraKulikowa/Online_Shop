using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface IFavouriteRepository
    {
        Task<Favourites> TryGetByIdAsync(string userId, int id);
        Task<List<Product>> GetAllAsync(string userId);
        Task AddAsync(Product product, string userId);
        Task DeleteFavouriteAsync(string userId, int id);
        Task ClearAsync(string userId);
    }
}