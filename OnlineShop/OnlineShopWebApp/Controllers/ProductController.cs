using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Repositories;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository products;
        private readonly ICompareRepository compareList;
        public ProductController(IProductsRepository products, ICompareRepository compareList)
        {
            this.products = products;
            this.compareList = compareList;
        }
        public IActionResult Index(int id)
        {
            var product = products.TryGetById(id);
            var productVM = Mapping.ToProductViewModel(product);
            return View(productVM);
        }
        public IActionResult Compare(int id)
        {
            var product = products.TryGetById(id);
            compareList.Add(product, Constants.UserId);
            return Redirect("~/Compare/Index/");
        }

        [HttpPost]
        public IActionResult SearchResult(string search)
        {
            var searchresult = products.Search(search);
            var searchresultVM = Mapping.ToProductViewModels(searchresult);
            return View(searchresultVM);
        }
    }
}