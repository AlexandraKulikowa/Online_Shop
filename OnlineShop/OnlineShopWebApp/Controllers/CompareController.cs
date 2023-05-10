using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Repositories;

namespace OnlineShopWebApp.Controllers
{
    public class CompareController : Controller
    {
        private readonly ICompareRepository compareList;
        public CompareController(ICompareRepository compareList)
        {
            this.compareList = compareList;
        }
        public IActionResult Index()
        {
            var list = compareList.TryGetByUserId(Constants.UserId);
            return View(list);
        }
        public IActionResult Delete(int id)
        {
            compareList.DeleteProduct(Constants.UserId, id);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            compareList.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}