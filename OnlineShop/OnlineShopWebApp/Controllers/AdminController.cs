using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductsRepository products;
        private readonly IOrderRepository orders;
        public AdminController(IProductsRepository products, IOrderRepository orders)
        {
            this.products = products;
            this.orders = orders;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders()
        {
            var listOrders = orders.Orders;
            return View(listOrders);
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
            if (product.Name == product.Description)
            {
                ModelState.AddModelError("", "Название товара не может совпадать с описанием!");
            }

            if (ModelState.IsValid)
            {
                products.Edit(product);
                return RedirectToAction("Products");
            }

            return View("EditProduct", product);
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (product.Name == product.Description)
            {
                ModelState.AddModelError("", "Название товара не может совпадать с описанием!");
            }

            if (ModelState.IsValid)
            {
                products.Add(product);
                return RedirectToAction("Products");
            }
            return View("AddProduct", product);
        }

        public IActionResult Delete(int id)
        {
            var product = products.TryGetById(id);
            products.Delete(product);
            return RedirectToAction("Products");
        }
        public IActionResult Details(int id)
        {
            var order = orders.GetOrder(id);
            return View(order);
        }

        [HttpPost]
        public IActionResult EditStatus(int id, Status status)
        {
            orders.ChangeStatus(id, status);
            return RedirectToAction("Orders");
        }
    }
}