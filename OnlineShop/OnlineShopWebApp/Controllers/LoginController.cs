using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsersRepository users;
        private readonly IRolesRepository roles;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public LoginController(IUsersRepository users, IRolesRepository roles, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.users = users;
            this.roles = roles;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index(string returnUrl)
        {
            return View(new Authorization() { ReturnUrl = returnUrl});
        }

        [HttpPost]
        public IActionResult Enter(Authorization authorization)
        {
            if (ModelState.IsValid)
            {
                var result = signInManager.PasswordSignInAsync(authorization.Login, authorization.Password, authorization.IsRemember, false).Result;
                if(result.Succeeded)
                {
                    return Redirect(authorization.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль");
                }
            }
            return View("Index", authorization);
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserViewModel registration)
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
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Registration", registration);
        }

        public IActionResult Logout()
        {
            signInManager.SignOutAsync().Wait();

            return RedirectToAction("~/Home/Index/");
        }
    }
}