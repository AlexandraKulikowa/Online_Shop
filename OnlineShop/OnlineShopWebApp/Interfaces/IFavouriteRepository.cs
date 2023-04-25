using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Interfaces
{
    public interface IFavouriteRepository
    {
        Favourites TryGetByUserId(string userId);
        void Add(Product product, string userId);
        void DeleteFavourite(string userId, int id);
        void Clear(string userId);
    }
}