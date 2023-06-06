using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();
            var usersVM = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userVM = await GetRolesVM(user);
                usersVM.Add(userVM);
            }
            return View(usersVM);
        }

        public async Task<IActionResult> DetailsAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userVM = await GetRolesVM(user);
            return View(userVM);
        }

        public async Task<IActionResult> DeleteAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user.UserName == "admin@gmail.com")
            {
                return RedirectToAction("Error");
            }
            await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        public IActionResult EditPassword(string id)
        {
            var passwordVM = new PasswordViewModel { Id = id };
            return View(passwordVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditPasswordAsync(PasswordViewModel passwordVM)
        {
            var user = await userManager.FindByIdAsync(passwordVM.Id);

            var checkOldPassword = await userManager.CheckPasswordAsync(user, passwordVM.OldPassword);
            if(!checkOldPassword)
            {
                ModelState.AddModelError("", "Вы неверно ввели старый пароль!");
            }

            var checkNew = await userManager.CheckPasswordAsync(user, passwordVM.NewPassword);
            if (checkNew)
            {
                ModelState.AddModelError("", "Пароль не должен быть идентичен старому!");
            }

            if (ModelState.IsValid)
            {
                await userManager.ChangePasswordAsync(user, passwordVM.OldPassword, passwordVM.NewPassword);
                var result = await userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View("EditPassword", passwordVM);
                }
                return RedirectToAction("Details", passwordVM);
            }
            return View("EditPassword", passwordVM);
        }

        public async Task<IActionResult> EditAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userVM = await GetRolesVM(user);
            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(UserViewModel userVM)
        {
            var user = await userManager.FindByIdAsync(userVM.Id);

            var checkLogin = await userManager.CheckPasswordAsync(user, userVM.Login);
            if (checkLogin)
            {
                ModelState.AddModelError("", "Логин и пароль не могут совпадать!");
            }

            if (ModelState.IsValid)
            {

                user.ChangeUser(userVM);
                var result = await userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View("Edit", userVM);
                }
            }
            return View("Details", userVM);
        }

        public async Task<IActionResult> RightsAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var roles = await userManager.GetRolesAsync(user);
            var rolesVM = roles.Select(x => new RoleViewModel { Name = x }).ToList();

            var allRoles = await roleManager.Roles.ToListAsync();
            var allRolesVM = allRoles.Select(x => x.ToRoleViewModel()).ToList();

            var rightsVM = user.ToRightsViewModel(rolesVM, allRolesVM);
            return View(rightsVM);
        }

        [HttpPost]
        public async Task<IActionResult> RightsAsync(string id, Dictionary<string, string> userRolesVM)
        {
            var userSelectedRoles = userRolesVM.Select(x => x.Key);

            var user = await userManager.FindByIdAsync(id);
            var roles = await userManager.GetRolesAsync(user);

            await userManager.RemoveFromRolesAsync(user, roles);
            await userManager.AddToRolesAsync(user, userSelectedRoles);

            var userVM = await GetRolesVM(user);
            return View("Details", userVM);
        }

        public async Task<UserViewModel> GetRolesVM(User user)
        {
            var roles = (await userManager.GetRolesAsync(user)).ToList();
            var userVM = user.ToUserViewModel();
            userVM.Roles = roles;
            return userVM;
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}