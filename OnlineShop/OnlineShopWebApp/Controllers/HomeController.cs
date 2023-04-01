using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Repositories;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private static ProductsRepository products;

        public HomeController()
        {
            products = new ProductsRepository();
        }

        public IActionResult Index()
        {
            return View(products.GetAll());
        }
    }
}