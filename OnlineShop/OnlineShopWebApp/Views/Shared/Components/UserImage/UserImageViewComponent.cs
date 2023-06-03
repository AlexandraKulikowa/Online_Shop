using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Views.Shared.Components.UserImage
{
    public class UserImageViewComponent : ViewComponent
    {
        private readonly UserManager<User> userManager;
        public UserImageViewComponent(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke(string name)
        {
            var user = userManager.FindByNameAsync(name).Result;
            var imagePath = user.ImagePath;
            return View("UserImage",imagePath);
        }
    }
}