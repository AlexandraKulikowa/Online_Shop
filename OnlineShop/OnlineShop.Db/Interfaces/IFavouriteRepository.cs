using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IFavouriteRepository
    {
        Favourites TryGetByUserId(string userId);
        void Add(ProductViewModel product, string userId);
        void DeleteFavourite(string userId, int id);
        void Clear(string userId);
    }
}