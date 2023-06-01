using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShop.Db.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class FavouriteController : Controller
    {
        private readonly IFavouriteRepository favourites;
        private readonly IProductsRepository products;
        public FavouriteController(IFavouriteRepository favourites, IProductsRepository products)
        {
            this.favourites = favourites;
            this.products = products;
        }
        public IActionResult Index()
        {
            var list = favourites.GetAll(Constants.UserId);
            var listVM = list.ToProductViewModels();
            return View(listVM);
        }

        public IActionResult Add(int id)
        {
            var product = products.TryGetById(id);
            favourites.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            favourites.DeleteFavourite(Constants.UserId, id);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            favourites.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}