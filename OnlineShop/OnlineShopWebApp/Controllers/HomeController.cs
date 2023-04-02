using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository products;

        public HomeController(IProductsRepository products)
        {
            this.products = products;
        }
        public IActionResult Index()
        {
            return View(products.ListProducts);
        }
    }
}