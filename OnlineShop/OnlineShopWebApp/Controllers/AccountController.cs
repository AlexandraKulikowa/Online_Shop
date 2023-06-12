using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System.Threading.Tasks;

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
        public async Task<IActionResult> EnterAsync(Authorization authorization)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(authorization.Login, authorization.Password, authorization.IsRemember, false);
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
        public async Task<IActionResult> RegisterAsync(RegistrationViewModel registration)
        {
            if (registration.Login == registration.Password)
                ModelState.AddModelError("", "Логин и пароль не могут совпадать!");

            var checkUser = await userManager.FindByNameAsync(registration.Login);
            if (checkUser != null)
            {
                ModelState.AddModelError("", "Такой пользователь уже зарегистрирован!");
            }

            if (ModelState.IsValid)
            {
                var user = registration.ToUser();
                var result = await userManager.CreateAsync(user, registration.Password);

                if (result.Succeeded)
                {
                    if (!await TryAssignUserRoleAsync(user))
                    {
                        ModelState.AddModelError("", "Что-то пошло не так. Роль пользователю не добавлена.");
                    }
                    if (!User.IsInRole(Constants.AdminRoleName))
                    { 
                        return RedirectToAction("SuccessRegistration");
                    }
                    return Redirect("~/Admin/User/Index/");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("Registration", registration);
        }

        public IActionResult SuccessRegistration()
        {
            return View();
        }

        private async Task<bool> TryAssignUserRoleAsync(User user)
        {
            var result = await userManager.AddToRoleAsync(user, Constants.UserRoleName);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await signInManager.SignOutAsync();
            return Redirect("~/Home/Index/");
        }

        public async Task<IActionResult> ProfileAsync(string name)
        {
            var user = await userManager.FindByNameAsync(name);
            var userVM = user.ToUserViewModel();
            return View(userVM);
        }

        public async Task<IActionResult> EditUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userVM = user.ToUserViewModel();
            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserAsync(UserViewModel userVM)
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
                user.ImagePath = createUserImage.CreateImage(userVM);
                var result = await userManager.UpdateAsync(user);
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

        public async Task<IActionResult> DeleteImageAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            user.ImagePath = null;
            await userManager.UpdateAsync(user);
            var userVM = user.ToUserViewModel();
            return RedirectToAction("EditUser", userVM);
        }

        public IActionResult Check(string id)
        {
            return View(new CheckViewModel() { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CheckAsync(CheckViewModel check)
        {
            var user = await userManager.FindByIdAsync(check.Id);
            var checkPassword = await userManager.CheckPasswordAsync(user, check.Password);
            if (!checkPassword)
            {
                ModelState.AddModelError("", "Пароль неверный!");
            }
            if (ModelState.IsValid)
            {
                var userVM = user.ToUserViewModel();
                return View("EditUser", userVM);
            }
            return View("Check", check);
        }
    }
}