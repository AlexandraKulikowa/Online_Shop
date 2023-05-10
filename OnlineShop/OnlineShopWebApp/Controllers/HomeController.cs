using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
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
            var productsDb = products.GetAll();
            var productsViewModels = Mapping.ToProductViewModels(productsDb);
            return View(productsViewModels);
        }
        public IActionResult Compare(int id)
        {
            var product = products.TryGetById(id);
            var productVM = Mapping.ToProductViewModel(product);
            compareList.Add(productVM, Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}