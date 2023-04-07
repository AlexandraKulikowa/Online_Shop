using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;

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
            var list = compareList.TryGetByUserId(Repositories.Constants.UserId);
            return View(list);
        }
        public IActionResult Delete(int id)
        {
            compareList.DeleteProduct(Repositories.Constants.UserId, id);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            compareList.Clear(Repositories.Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}