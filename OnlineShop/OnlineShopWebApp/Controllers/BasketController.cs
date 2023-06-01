using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IProductsRepository products;
        private readonly IBasketsRepository baskets;
        public BasketController(IProductsRepository products, IBasketsRepository baskets)
        {
            this.products = products;
            this.baskets = baskets;
        }
        public IActionResult Index()
        {
            var basket = baskets.TryGetByUserId(Constants.UserId);
            var basketVM = basket.ToBasketViewModel();
            return View(basketVM);
        }
        public IActionResult Add(int id)
        {
            var product = products.TryGetById(id);
            baskets.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult ChangeAmount(int id, bool sign)
        {
            baskets.ChangeAmount(id, sign, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            baskets.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}