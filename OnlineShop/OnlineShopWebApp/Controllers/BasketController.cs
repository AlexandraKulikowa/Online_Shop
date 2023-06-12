using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var basket = await baskets.TryGetByUserIdAsync(Constants.UserId);
            var basketVM = basket.ToBasketViewModel();
            return View(basketVM);
        }
        public async Task<IActionResult> AddAsync(int id)
        {
            var product = await products.TryGetByIdAsync(id);
            await baskets.AddAsync(product, Constants.UserId);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ChangeAmount(int id, bool sign)
        {
            await baskets.ChangeAmountAsync(id, sign, Constants.UserId);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Clear()
        {
            await baskets.ClearAsync(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}