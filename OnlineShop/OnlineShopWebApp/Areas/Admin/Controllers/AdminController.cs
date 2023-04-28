using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IProductsRepository products;
        private readonly IOrderRepository orders;
        private readonly IRolesRepository roles;
        private readonly IUsersRepository users;
        public AdminController(IProductsRepository products, IOrderRepository orders, IRolesRepository roles, IUsersRepository users)
        {
            this.products = products;
            this.orders = orders;
            this.roles = roles;
            this.users = users;
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
            var listUsers = users.GetAll();
            return View(listUsers);
        }

        public IActionResult Roles()
        {
            var rolesList = roles.GetAll();
            return View(rolesList);
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
            if (!products.CheckNewProduct(product))
                ModelState.AddModelError("", "Название товара не может совпадать с описанием!");

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
            if (!products.CheckNewProduct(product))
                ModelState.AddModelError("", "Название товара не может совпадать с описанием!");

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

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRole(Role role)
        {

            if (role.Name == role.Options)
            {
                ModelState.AddModelError("", "Наименование роли не может совпадать с описанием её функций!");
            }

            if (!roles.CheckRole(role))
                ModelState.AddModelError("", "Такая роль уже есть!");

            if (ModelState.IsValid)
            {
                roles.Add(role);
                return RedirectToAction("Roles");
            }
            return View(role);
        }

        public IActionResult DeleteRole(int id)
        {
            roles.Delete(id);
            return RedirectToAction("Roles");
        }
    }
}