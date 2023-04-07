using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Controllers
{
    public class CompareController : Controller
    {
        private readonly IProductsRepository products;
        private readonly ICompareRepository productsForCompare;
        public CompareController(IProductsRepository products, ICompareRepository productsForCompare)
        {
            this.products = products;
            this.productsForCompare = productsForCompare;
        }
        public IActionResult Index()
        {
            return View(productsForCompare.ProductsForCompare);
        }

        public IActionResult Delete(int id)
        {
            productsForCompare.DeleteProduct(id);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            productsForCompare.Clear();
            return RedirectToAction("Index");
        }
    }
}
