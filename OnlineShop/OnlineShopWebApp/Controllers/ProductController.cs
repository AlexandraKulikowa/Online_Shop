using Microsoft.AspNetCore.Mvc;
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
            return View(products.TryGetById(id));
        }
        public IActionResult Compare(int productId)
        {
            compareList.Add(products.TryGetById(productId), Constants.UserId);
            return Redirect("~/Compare/Index/");
        }
    }
}