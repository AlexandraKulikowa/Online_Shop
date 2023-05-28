using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
                    return Redirect("~/Home/Index/");
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

        public IActionResult Logout()
        {
            signInManager.SignOutAsync().Wait();
            return Redirect("~/Home/Index/");
        }
    }
}