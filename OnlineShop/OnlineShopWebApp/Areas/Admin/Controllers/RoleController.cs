using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using System.Linq;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            var rolesVM = roles.Select(x => x.ToRoleViewModel()).ToList();
            return View(rolesVM);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(RoleViewModel roleVM)
        {
            var check = roleManager.RoleExistsAsync(roleVM.Name).Result;
            if (check)
                ModelState.AddModelError("", "Такая роль уже есть!");

            if (ModelState.IsValid)
            {
                var role = new IdentityRole { Name = roleVM.Name };
                var result = roleManager.CreateAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(roleVM);
        }

        public IActionResult Delete(string name)
        {
            var role = roleManager.FindByNameAsync(name).Result;
            if (role == null)
            {
                ModelState.AddModelError("", "Невозможно удалить несуществующую роль!");
            }
            if (role.Name == "Admin")
            {
                return Redirect("~/Admin/User/Error/");
            }

            var users = userManager.Users.ToList();
            foreach (var user in users)
            {
                var listRoles = userManager.GetRolesAsync(user).Result;
                var checkRole = listRoles.Where(x => x == name).ToList();
                if (checkRole.Any())
                {
                    ModelState.AddModelError("", "Эта роль присвоена пользователю! Нельзя её удалить!");
                    break;
                }
            }

            if (ModelState.IsValid)
            {
                roleManager.DeleteAsync(role).Wait();
            }
            return RedirectToAction("Index");
        }
    }
}