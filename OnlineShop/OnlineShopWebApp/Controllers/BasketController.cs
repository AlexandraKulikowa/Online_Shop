using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp.Controllers
{
    public class BasketController : Controller
    {
        private static ProductRepository products;
        public Product Product { get; set; }
        private Basket basket = new Basket();

        private List<SumProducts> productsInBasket;
        public BasketController()
        {
            products = new ProductRepository();
            productsInBasket = basket.GetProducts();
        }
        public IActionResult Index(int id)
        {
            if (id != 0)
            {
                Product = products.TryGetById(id);
                var number = 1; // потом реализую это через кнопку плюс и минус
                var NumberOfProducts = new SumProducts(Product.Name, Product.Cost, Product.Description, Product.Genre, Product.PaintingTechnique, Product.Size, Product.Year, Product.IsPromo, number);
                productsInBasket.Add(NumberOfProducts);
                return View(productsInBasket);
            }
            return View(productsInBasket);
        }
    }
}
