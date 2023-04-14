using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductsRepository products;
        public AdminController(IProductsRepository products)
        {
            this.products = products;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Orders()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Roles()
        {
            return View();
        }
        public IActionResult Products()
        {
            return View(products.GetAll());
        }

        public IActionResult AddProduct()
        {
            return View();
        }
        public IActionResult EditProduct(int id)
        {
            var product = products.TryGetById(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product product)
        {
            products.Edit(product);
            return RedirectToAction("Products");
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            products.Add(product);
            return RedirectToAction("Products");
        }
        public IActionResult Delete(int id)
        {
            var product = products.TryGetById(id);
            products.Delete(product);
            return RedirectToAction("Products");
        }
    }
}