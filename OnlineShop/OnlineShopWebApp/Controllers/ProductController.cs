using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Repositories;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private static ProductsRepository products;
        public ProductController()
        {
            products = new ProductsRepository();
        }
        public IActionResult Index(int id)
        {
            return View(products.TryGetById(id));
        }
    }
}