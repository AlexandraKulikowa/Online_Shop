using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
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