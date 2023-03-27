using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private static ProductRepository products;
        public ProductController()
        {
            products = new ProductRepository();
        }
        public IActionResult Index(int id)
        {
            return View(products.TryGetById(id));
        }
    }
}
