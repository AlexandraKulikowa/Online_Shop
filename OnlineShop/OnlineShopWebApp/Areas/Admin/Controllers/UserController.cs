using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;

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

        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            return View(users.ToUserViewModels());
        }

        public IActionResult Details(string id)
        {
            var user = userManager.FindByIdAsync(id).Result;
            var userVM = user.ToUserViewModel();
            return View(userVM);
        }

        public IActionResult Delete(string id)
        {
            var user = userManager.FindByIdAsync(id).Result;
            userManager.DeleteAsync(user).Wait();
            return RedirectToAction("Index");
        }

        public IActionResult EditPassword(string id)
        {
            var passwordVM = new PasswordViewModel { Id = id };
            return View(passwordVM);
        }

        [HttpPost]
        public IActionResult EditPassword(PasswordViewModel passwordVM)
        {
            var user = userManager.FindByIdAsync(passwordVM.Id).Result;
            var userVM = new UserViewModel();

            var checkOld = userManager.CheckPasswordAsync(user, passwordVM.OldPassword).Result;
            if (!checkOld)
            {
                ModelState.AddModelError("", "Вы неверно ввели старый пароль!");
            }

            var checkNew = userManager.CheckPasswordAsync(user, passwordVM.NewPassword).Result;
            if (checkNew)
            {
                ModelState.AddModelError("", "Пароль не должен быть идентичен старому!");
            }

            if (ModelState.IsValid)
            {
                userManager.ChangePasswordAsync(user, passwordVM.OldPassword, passwordVM.NewPassword).Wait();
                var result = userManager.UpdateAsync(user).Result;

                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View("EditPassword", passwordVM);
                }
                user = userManager.FindByIdAsync(user.Id).Result;
                userVM = user.ToUserViewModel();
                return RedirectToAction("Details", passwordVM);
            }
            userVM = user.ToUserViewModel();
            return View("EditPassword", passwordVM);
        }

        public IActionResult Edit(string id)
        {
            var user = userManager.FindByIdAsync(id).Result;
            var userVM = user.ToUserViewModel();
            return View(userVM);
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel userVM)
        {
            var user = userManager.FindByIdAsync(userVM.Id).Result;
            var checkLogin = userManager.CheckPasswordAsync(user, userVM.Login).Result;
            if (checkLogin)
            {
                ModelState.AddModelError("", "Логин и пароль не могут совпадать!");
            }

            if (ModelState.IsValid)
            {

                user.ChangeUser(userVM);
                var result = userManager.UpdateAsync(user).Result;
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View("Details", userVM);
                }
            }
            return View("Details", userVM);
        }

        public IActionResult Rights(string id)
        {
            var user = userManager.FindByIdAsync(id).Result;
            var roles = userManager.GetRolesAsync(user).Result;
            var rolesVM = roles.Select(x => new RoleViewModel { Name = x }).ToList();

            var allRoles = roleManager.Roles.ToList();
            var allRolesVM = allRoles.Select(x => x.ToRoleViewModel()).ToList();

            var rightsVM = user.ToRightsViewModel(rolesVM, allRolesVM);
            return View(rightsVM);
        }

        [HttpPost]

        public IActionResult Rights(string id, Dictionary<string, string> userRolesVM)
        {
            var userSelectedRoles = userRolesVM.Select(x => x.Key);

            var user = userManager.FindByIdAsync(id).Result;
            var roles = userManager.GetRolesAsync(user).Result;

            userManager.RemoveFromRolesAsync(user, roles).Wait();
            userManager.AddToRolesAsync(user, userSelectedRoles).Wait();

            return RedirectToAction("Rights");
        }
    }
}