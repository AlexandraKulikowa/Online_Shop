using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;

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
            var product = products.TryGetById(id);
            var productVM = product.ToProductViewModel();
            return View(productVM);
        }

        [HttpPost]
        public IActionResult SearchResult(string search)
        {
            var searchresult = products.Search(search);
            var searchresultVM = searchresult.ToProductViewModels();
            return View(searchresultVM);
        }
    }
}