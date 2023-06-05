using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System.IO;
using System;
using System.Linq;
using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private readonly IProductsRepository products;
        private readonly IWebHostEnvironment appEnvironment;
        public ProductController(IProductsRepository products, IWebHostEnvironment appEnvironment)
        {
            this.products = products;
            this.appEnvironment = appEnvironment;
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
            var product = CreateProduct(productVM);

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
            var product = CreateProduct(productVM);

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

        public Product CreateProduct(ProductViewModel productVM)
        {
            productVM.ImagePath = new List<string>();
            if (productVM.UploadedFiles != null)
            {
                var imagesPath = Path.Combine(appEnvironment.WebRootPath + "/images/products/");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                foreach (var file in productVM.UploadedFiles)
                {
                    var fileName = Guid.NewGuid() + "." + file.FileName.Split('.').Last();
                    using (var fileStream = new FileStream(imagesPath + fileName, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.ImagePath.Add("/images/products/" + fileName);
                }
            }
            return productVM.ToProduct();
        }
    }
}