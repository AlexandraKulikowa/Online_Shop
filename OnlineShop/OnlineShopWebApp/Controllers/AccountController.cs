using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersRepository users;
        private readonly IRolesRepository roles;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(IUsersRepository users, IRolesRepository roles, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.users = users;
            this.roles = roles;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index(string returnUrl)
        {
            return View( new Authorization() { ReturnUrl = returnUrl});
        }

        [HttpPost]
        public IActionResult Enter(Authorization authorization)
        {
            var containsNoUser = users.CheckUser(authorization.Login, authorization.Password);
            if (containsNoUser)           
                ModelState.AddModelError("", "Данные введены неверно! Проверьте правильность набора логина и пароля. Или, может, вы не зарегистрированы?");

            if (ModelState.IsValid)
            {
                var result = signInManager.PasswordSignInAsync(authorization.Login, authorization.Password, authorization.IsRemember, false).Result;
                if(result.Succeeded)
                {
                    return Redirect(authorization.ReturnUrl);
                }
            }
            return View("Index", authorization);
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User registration)
        {
            if (registration.Login == registration.Password)
                ModelState.AddModelError("", "Логин и пароль не могут совпадать!");

            var containsNoUser = users.CheckUser(registration.Login, registration.Password);
            if (!containsNoUser)
                ModelState.AddModelError("", "Пользователь уже зарегистрирован! Введите другие данные");

            if (ModelState.IsValid)
            {
                Role userRole = roles.TryGetById(2);
                registration.Role = userRole;
                users.Add(registration);
                return Redirect("~/Home/Index/");
            }
            return View("Registration", registration);
        }
    }
}