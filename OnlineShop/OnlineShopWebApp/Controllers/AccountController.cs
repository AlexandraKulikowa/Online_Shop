using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new Authorization() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public IActionResult Enter(Authorization authorization)
        {
            if (ModelState.IsValid)
            {
                var result = signInManager.PasswordSignInAsync(authorization.Login, authorization.Password, authorization.IsRemember, false).Result;
                if (result.Succeeded)
                {
                    if (authorization.ReturnUrl != null)
                    {
                        return Redirect(authorization.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Неправильный пароль");
            }
            return View("Login", authorization);
        }

        public IActionResult Registration(string returnUrl)
        {
            return View(new RegistrationViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public IActionResult Register(RegistrationViewModel registration)
        {
            if (registration.Login == registration.Password)
                ModelState.AddModelError("", "Логин и пароль не могут совпадать!");

            if (ModelState.IsValid)
            {
                var user = registration.ToUser();
                var result = userManager.CreateAsync(user, registration.Password).Result;

                if (result.Succeeded)
                {
                    signInManager.SignInAsync(user, false).Wait();
                    if (!TryAssignUserRole(user))
                    {
                        ModelState.AddModelError("", "Что-то пошло не так. Роль пользователю не добавлена.");
                    }


                        if (registration.ReturnUrl != null)
                            return Redirect(registration.ReturnUrl);

                        return Redirect("~/Home/Index/");
                    }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("Registration", registration);
        }

        private bool TryAssignUserRole(User user)
        {
            var result = userManager.AddToRoleAsync(user, Constants.UserRoleName).Result;
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public IActionResult Logout()
        {
            signInManager.SignOutAsync().Wait();
            return Redirect("~/Home/Index/");
        }
    }
}