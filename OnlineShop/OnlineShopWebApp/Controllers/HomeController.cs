﻿using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository products;
        private readonly ICompareRepository productsForCompare;
        public HomeController(IProductsRepository products, ICompareRepository productsForCompare)
        { 
            this.products = products;
            this.productsForCompare = productsForCompare;
        }
        public IActionResult Index()
        {
            return View(products.ListProducts);
        }
        public IActionResult Compare(int productId)
        {
            var product = products.TryGetById(productId);
            productsForCompare.Add(product);
            return RedirectToAction("Index");
        }
    }
}