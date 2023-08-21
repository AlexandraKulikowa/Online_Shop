using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        IUnitOfWork unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(int id)
        {
            var product = await unitOfWork.ProductsDbRepository.TryGetByIdAsync(id);
            var productVM = product.ToProductViewModel();
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> SearchResultAsync(string search)
        {
            var searchresult = await unitOfWork.ProductsDbRepository.SearchAsync(search);
            var searchresultVM = searchresult.ToProductViewModels();
            return View(searchresultVM);
        }
    }
}