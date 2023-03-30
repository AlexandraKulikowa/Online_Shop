using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp.Controllers
{
    public class BasketController : Controller
    {
        private static ProductRepository products;
        private static Basket basket;

        private List<ProductWithQuantity> productsInBasket;
        public BasketController()
        {
            products = new ProductRepository();
            basket = new Basket();
            productsInBasket = basket.GetProducts();
        }
        public IActionResult Index(int id)
        {
            if (id != 0)
            {
                var Product = products.TryGetById(id);
                var count = 1; // потом реализую это через кнопку плюс и минус
                var BasketItem = new ProductWithQuantity(Product.Name, Product.Cost, Product.Description, Product.Genre, Product.PaintingTechnique, Product.Size, Product.Year, Product.IsPromo, count);
                basket.AddProduct(BasketItem);
                return View(productsInBasket);
            }
            ViewBag.TotalCost = basket.TotalCost();
            return View(productsInBasket);
        }
    }
}
