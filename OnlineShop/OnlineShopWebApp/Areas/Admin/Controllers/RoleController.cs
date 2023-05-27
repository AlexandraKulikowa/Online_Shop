using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Interfaces;
using System.Data;
using System.Linq;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RoleController : Controller
    {
        private readonly RoleManager<Role> roleManager;
        public RoleController(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var rolesList = roleManager.Roles.ToList();
            return View(rolesList);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Role role)
        {
            var check = roleManager.RoleExistsAsync(role.Name).Result;
            if(check)
                ModelState.AddModelError("", "Такая роль уже есть!");

            if (ModelState.IsValid)
            {
                roleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }
            return View(role);
        }

        public IActionResult Delete(string id)
        {
            var role = roleManager.FindByIdAsync(id).Result;
            roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}