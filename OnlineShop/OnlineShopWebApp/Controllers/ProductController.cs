using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository products;
        private readonly ICompareRepository productsForCompare;
        public ProductController(IProductsRepository products, ICompareRepository productsForCompare)
        {
            this.products = products;
            this.productsForCompare = productsForCompare;
        }
        public IActionResult Index(int id)
        {
            return View(products.TryGetById(id));
        }
        public IActionResult Compare(int productId)
        {
            productsForCompare.Add(products.TryGetById(productId));
            return Redirect("~/Compare/Index/");
        }
    }
}