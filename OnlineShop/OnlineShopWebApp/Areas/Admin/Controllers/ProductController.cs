﻿using Microsoft.AspNetCore.Mvc;
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
            var product = productVM.ToProduct();

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
            var product = productVM.ToProduct();

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