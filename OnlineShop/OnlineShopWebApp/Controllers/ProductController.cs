using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository products;
        public ProductController(IProductsRepository products)
        {
            this.products = products;
        }
        public async Task<IActionResult> Index(int id)
        {
            var product = await products.TryGetByIdAsync(id);
            var productVM = product.ToProductViewModel();
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> SearchResultAsync(string search)
        {
            var searchresult = await products.SearchAsync(search);
            var searchresultVM = searchresult.ToProductViewModels();
            return View(searchresultVM);
        }
    }
}