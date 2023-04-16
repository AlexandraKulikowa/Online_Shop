using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;
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
        public IActionResult Search(Search search)
        {
            var product = products.Search(search.Name);
            if (product == null)
                return RedirectToAction("EmptySearch");
            return RedirectToAction("Index", "Product", new { id = product.Id });
        }
        public IActionResult EmptySearch() 
        {
            return View();
        }
    }
}