using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System.Linq;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        public UserController(UserManager<User> userManager)
        {
            this.userManager = userManager;
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

            var checkOldPassword = userManager.CheckPasswordAsync(user, passwordVM.OldPassword).Result;
            if(!checkOldPassword)
            {
                ModelState.AddModelError("", "Вы неверно ввели старый пароль!");
            }

            var checkNew = userManager.CheckPasswordAsync(user, passwordVM.NewPassword).Result;
            if(checkNew)
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
                    return View("Edit", userVM);
                }
            }
            return View("Details", userVM);
        }
    }
}