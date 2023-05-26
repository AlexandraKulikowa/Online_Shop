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
            var listUsers = userManager.Users.ToList();
            return View(listUsers.ToUserViewModels());
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
            userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        public IActionResult EditPassword(string id)
        {
            var user = userManager.FindByIdAsync(id).Result;
            var userVM = user.ToUserViewModel();
            return View(userVM);
        }

        [HttpPost]
        public IActionResult EditPassword(string id, string oldPassword, string password, string confirmPassword)
        {
            if (password != confirmPassword)
                ModelState.AddModelError("", "Пароли не совпадают!");

            if (oldPassword == password)
                ModelState.AddModelError("", "Новый пароль не должен быть идентичен старому!");


            var user = userManager.FindByIdAsync(id).Result;

            if (ModelState.IsValid)
            {
                userManager.ChangePasswordAsync(user, oldPassword, password).Wait();
                user.NormalizedPassword = password;
                var result = userManager.UpdateAsync(user).Result;
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                        var userVMMM = user.ToUserViewModel();
                        return View("EditPassword", userVMMM);
                    }
                }
                user = userManager.FindByIdAsync(id).Result;
                var userVMM = user.ToUserViewModel();
                return RedirectToAction("Details", userVMM);
            }
            var userVM = user.ToUserViewModel();
            return View("EditPassword", userVM);
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
            if (userVM.Login == userVM.Password)
                ModelState.AddModelError("", "Логин и пароль не могут совпадать!");

            if (ModelState.IsValid)
            {
                var user = userManager.FindByIdAsync(userVM.Id).Result;
                user.ChangeUser(userVM);
                var result = userManager.UpdateAsync(user).Result;
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                        return View("Details", userVM);
                    }
                }
                return RedirectToAction("Details", userVM);
            }
            return View("Edit", userVM);
        }
    }
}