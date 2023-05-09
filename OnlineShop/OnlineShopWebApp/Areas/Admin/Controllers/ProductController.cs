using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductsRepository products;
        public ProductController(IProductsRepository products) => this.products = products;

        public IActionResult Index()
        {
            var productlist = products.GetAll();
            var productsVM = Mapping.ToProductViewModels(productlist);
            return View(productsVM);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var product = products.TryGetById(id);
            var productVM = Mapping.ToProductViewModel(product);
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel productVM)
        {
            var product = Mapping.ToProduct(productVM);

            if (!products.CheckNewProduct(product))
                ModelState.AddModelError("", "Название товара не может совпадать с описанием!");

            if (ModelState.IsValid)
            {
                products.Edit(product);
                return RedirectToAction("Index");
            }

            return View("Edit", productVM);
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productVM)
        {
            var product = Mapping.ToProduct(productVM);

            if (!products.CheckNewProduct(product))
                ModelState.AddModelError("", "Название товара не может совпадать с описанием!");

            if (ModelState.IsValid)
            {
                products.Add(product);
                return RedirectToAction("Index");
            }
            return View("Add", productVM);
        }
        public IActionResult Delete(int id)
        {
            var product = products.TryGetById(id);
            products.Delete(product);
            return RedirectToAction("Index");
        }
    }
}