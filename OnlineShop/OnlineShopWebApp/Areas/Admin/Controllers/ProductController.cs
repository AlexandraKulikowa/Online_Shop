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

        public async Task<IActionResult> Index()
        {
            var productlist = await products.GetAllAsync();
            var productsVM = productlist.ToProductViewModels();
            return View(productsVM);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProductViewModel productVM)
        {
            var product = createProductHelper.CreateProduct(productVM);

            if (!products.CheckNewProduct(product))
                ModelState.AddModelError("", "Название товара не может совпадать с описанием!");

            if (ModelState.IsValid)
            {
                await products.AddAsync(product);
                return RedirectToAction("Index");
            }
            return View("AddAsync", productVM);
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            var product = await products.TryGetByIdAsync(id);
            var productVM = product.ToProductViewModel();
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(ProductViewModel productVM)
        {
            var product = createProductHelper.CreateProduct(productVM);

            if (!products.CheckNewProduct(product))
                ModelState.AddModelError("", "Название товара не может совпадать с описанием!");

            if (ModelState.IsValid)
            {
                await products.EditAsync(product);
                return RedirectToAction("Index");
            }

            return View("EditAsync", productVM);
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var product = await products.TryGetByIdAsync(id);
            await products.DeleteAsync(product);
            return RedirectToAction("Index");
        }

    }
}