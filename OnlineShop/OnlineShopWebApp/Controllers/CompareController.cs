using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShop.Db.Repositories;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult Index()
        {
            var products = compareItem.GetAll(Constants.UserId);
            var listVM = products.ToProductViewModels();
            return View(listVM);
        }
        public IActionResult Add(int id)
        {
            var product = products.TryGetById(id);
            compareItem.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            compareItem.DeleteProduct(Constants.UserId, id);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            compareItem.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}