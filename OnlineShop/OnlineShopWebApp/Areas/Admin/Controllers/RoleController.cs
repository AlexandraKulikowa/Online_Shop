using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IRolesRepository roles;

        public RoleController(IRolesRepository roles) => this.roles = roles;

        public IActionResult Index()
        {
            var rolesList = roles.GetAll();
            return View(rolesList);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Role role)
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
                return RedirectToAction("Index");
            }
            return View(role);
        }

        public IActionResult Delete(int id)
        {
            roles.Delete(id);
            return RedirectToAction("Index");
        }
    }
}