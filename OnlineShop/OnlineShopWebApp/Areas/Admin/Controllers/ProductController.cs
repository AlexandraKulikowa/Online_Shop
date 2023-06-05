using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private readonly IProductsRepository products;
        private readonly CreateProductHelper createProductHelper;
        public ProductController(IProductsRepository products, CreateProductHelper createProductHelper)
        {
            this.products = products;
            this.createProductHelper = createProductHelper;
        }

        public IActionResult Index()
        {
            var productlist = products.GetAll();
            var productsVM = productlist.ToProductViewModels();
            return View(productsVM);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productVM)
        {
            var product = createProductHelper.CreateProduct(productVM);

            if (!products.CheckNewProduct(product))
                ModelState.AddModelError("", "Название товара не может совпадать с описанием!");

            if (ModelState.IsValid)
            {
                products.Add(product);
                return RedirectToAction("Index");
            }
            return View("Add", productVM);
        }

        public IActionResult Edit(int id)
        {
            var product = products.TryGetById(id);
            var productVM = product.ToProductViewModel();
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productVM)
        {
            var product = createProductHelper.CreateProduct(productVM);

            if (!products.CheckNewProduct(product))
                ModelState.AddModelError("", "Название товара не может совпадать с описанием!");

            if (ModelState.IsValid)
            {
                products.Edit(product);
                return RedirectToAction("Index");
            }

            return View("Edit", productVM);
        }

        public IActionResult Delete(int id)
        {
            var product = products.TryGetById(id);
            products.Delete(product);
            return RedirectToAction("Index");
        }

    }
}