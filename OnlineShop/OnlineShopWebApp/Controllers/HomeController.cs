using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;

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
            var productsDb = products.GetAll();
            var productsViewModels = Mapping.ToProductViewModels(productsDb);
            return View(productsViewModels);
        }
    }
}