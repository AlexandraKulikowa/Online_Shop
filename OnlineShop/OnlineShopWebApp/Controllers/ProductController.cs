using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository products;
        public ProductController(IProductsRepository products)
        {
            this.products = products;
        }
        public IActionResult Index(int id)
        {
            return View(products.TryGetById(id));
        }
    }
}