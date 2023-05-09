using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IBasketsRepository
    {
        Basket TryGetByUserId(string userId);
        void Add(Product product, string userId);
        void ChangeAmount(int id, bool sign, string userId);
        void ChangeAmountInBasketItem(bool sign, BasketItem ProductForChange);
        void ClearItem(string userId, int id);
        void Clear(string userId);
    }
}