using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();
            var rolesVM = roles.Select(x => x.ToRoleViewModel()).ToList();
            return View(rolesVM);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(RoleViewModel roleVM)
        {
            var check = await roleManager.RoleExistsAsync(roleVM.Name);
            if (check)
                ModelState.AddModelError("", "Такая роль уже есть!");

            if (ModelState.IsValid)
            {
                var role = new IdentityRole { Name = roleVM.Name };
                var result = await roleManager.CreateAsync(role);
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

        public async Task<IActionResult> Delete(string name)
        {
            var role = await roleManager.FindByNameAsync(name);
            if(role == null || role.Name == null))
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
                    return RedirectToAction("DeleteError");
                }
            }

            if (ModelState.IsValid)
            {
                await roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteError()
        {
            return View();
        }
    }
}