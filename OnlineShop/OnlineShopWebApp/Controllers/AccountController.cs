using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;
using System.Linq;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly CreateUserImage createUserImage;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, CreateUserImage createUserImage)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.createUserImage = createUserImage;
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

            var listNames = userManager.Users.Select(x => x.UserName).ToList();
            var check = true;
            foreach (var item in listNames)
            {
                if (item == registration.Login)
                {
                    check = false; break;
                }
            }
            if (check == false)
            {
                ModelState.AddModelError("", "Такой пользователь уже зарегистрирован!");
            }

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

        public IActionResult Profile(string name)
        {
            var user = userManager.FindByNameAsync(name).Result;
            var userVM = user.ToUserViewModel();
            return View(userVM);
        }

        public IActionResult EditUser(string id)
        {
            var user = userManager.FindByIdAsync(id).Result;
            var userVM = user.ToUserViewModel();
            return View(userVM);
        }

        [HttpPost]
        public IActionResult EditUser(UserViewModel userVM)
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
                user.ImagePath = createUserImage.CreateImage(userVM);
                var result = userManager.UpdateAsync(user).Result;
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View("EditUser", userVM);
                }
            }
            userVM = user.ToUserViewModel();
            return View("Profile", userVM);
        }

        public IActionResult DeleteImage(string id)
        {
            var user = userManager.FindByIdAsync(id).Result;
            user.ImagePath = null;
            userManager.UpdateAsync(user).Wait();
            var userVM = user.ToUserViewModel();
            return RedirectToAction("EditUser", userVM);
        }
    }
}