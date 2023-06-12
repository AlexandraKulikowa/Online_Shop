using OnlineShop.Db.Models;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface IBasketsRepository
    {
        Task<Basket> TryGetByUserIdAsync(string userId);
        Task AddAsync(Product product, string userId);
        Task ChangeAmountAsync(int id, bool sign, string userId);
        void ChangeAmountInBasketItem(bool sign, BasketItem ProductForChange);
        Task ClearItemAsync(string userId, int id);
        Task ClearAsync(string userId);
    }
}