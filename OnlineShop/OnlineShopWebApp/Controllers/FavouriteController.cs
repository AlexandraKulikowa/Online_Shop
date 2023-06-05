using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShop.Db.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var list = await favourites.GetAllAsync(Constants.UserId);
            var listVM = list.ToProductViewModels();
            return View(listVM);
        }

        public async Task<IActionResult> AddAsync(int id)
        {
            var product = await products.TryGetByIdAsync(id);
            await favourites.AddAsync(product, Constants.UserId);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await favourites.DeleteFavouriteAsync(Constants.UserId, id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ClearAsync()
        {
            await favourites.ClearAsync(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}