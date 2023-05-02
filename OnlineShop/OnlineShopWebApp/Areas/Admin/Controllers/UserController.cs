using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUsersRepository users;
        public UserController(IUsersRepository users) => this.users = users;

        public IActionResult Index()
        {
            var listUsers = users.GetAll();
            return View(listUsers);
        }

        public IActionResult Details(int id)
        {
            var user = users.TryGetById(id);
            return View(user);
        }
        
        public IActionResult Delete(int id)
        {
            users.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult EditPassword(int id)
        {
            var user = users.TryGetById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditPassword(int id, string password, string confirmpassword)
        {
            if (users.arePasswordsEqual(id, password))
                ModelState.AddModelError("", "Новый пароль не должен быть идентичен старому!");

            var user = users.TryGetById(id);
            if (ModelState.IsValid)
            {
                users.ChangePassword(id, password, confirmpassword);
                return RedirectToAction("Details", user);
            }
            return View("EditPassword", user);
        }

        public IActionResult Edit(int id)
        {
            var user = users.TryGetById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (user.Login == user.Password)
                ModelState.AddModelError("", "Логин и пароль не могут совпадать!");

            if (ModelState.IsValid)
            {
                users.ChangeUser(user);
                return RedirectToAction("Details", user);
            }
            return View("Edit", user); 
        }

    }
}