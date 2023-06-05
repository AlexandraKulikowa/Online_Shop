using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShop.Db.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CompareController : Controller
    {
        private readonly ICompareRepository compareItem;
        private readonly IProductsRepository products;
        public CompareController(ICompareRepository compareItem, IProductsRepository products)
        {
            this.compareItem = compareItem;
            this.products = products;
        }
        public async Task<IActionResult> Index()
        {
            var products = await compareItem.GetAllAsync(Constants.UserId);
            var listVM = products.ToProductViewModels();
            return View(listVM);
        }
        public async Task<IActionResult> AddAsync(int id)
        {
            var product = await products.TryGetByIdAsync(id);
            await compareItem.AddAsync(product, Constants.UserId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            await compareItem.DeleteProductAsync(Constants.UserId, id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ClearAsync()
        {
            await compareItem.ClearAsync(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}