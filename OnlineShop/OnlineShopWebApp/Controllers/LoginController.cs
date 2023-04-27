using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsersRepository users;
        public LoginController(IUsersRepository users)
        {
            this.users = users;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Enter(Authorization authorization)
        {
            var containsNoUser = users.CheckUser(authorization.Login, authorization.Password);
            if (containsNoUser)           
                ModelState.AddModelError("", "Данные введены неверно! Проверьте правильность набора логина и пароля. Или, может, вы не зарегистрированы?");

            if (ModelState.IsValid)
                return Redirect("~/Home/Index/");

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
                users.Add(registration);
                return Redirect("~/Home/Index/");
            }
            return View("Registration", registration);
        }
    }
}