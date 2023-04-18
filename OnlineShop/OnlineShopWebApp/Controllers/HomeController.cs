using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Repositories;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository products;
        private readonly ICompareRepository compareList;
        public HomeController(IProductsRepository products, ICompareRepository compareList)
        { 
            this.products = products;
            this.compareList = compareList;
        }
        public IActionResult Index()
        {
            return View(products.GetAll());
        }
        public IActionResult Compare(int productId)
        {
            var product = products.TryGetById(productId);
            compareList.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}