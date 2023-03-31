using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Repositories;

namespace OnlineShopWebApp.Controllers
{
    public class BasketController : Controller
    {
        private readonly ProductsRepository products;
        public BasketController()
        {
            products = new ProductsRepository();
        }
        public IActionResult Index()
        {
            var basket = BasketsRepository.TryGetByUserId(Constants.UserId);
            return View(basket);
        }
        public IActionResult Add(int id)
        {
            var product = products.TryGetById(id);
            BasketsRepository.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}
