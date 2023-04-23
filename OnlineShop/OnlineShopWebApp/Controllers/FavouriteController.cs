using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Repositories;

namespace OnlineShopWebApp.Controllers
{
    public class FavouriteController : Controller
    {
        private readonly IFavouriteRepository favourites;
        private readonly IProductsRepository products;
        public FavouriteController(IFavouriteRepository favouriteList, IProductsRepository products)
        {
            this.favourites = favouriteList;
            this.products = products;
        }
        public IActionResult Index()
        {
            var list = favourites.TryGetByUserId(Constants.UserId);
            return View(list);
        }

        public IActionResult Add(int id)
        {
            var product = products.TryGetById(id);
            favourites.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            favourites.DeleteProduct(Constants.UserId, id);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            favourites.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}