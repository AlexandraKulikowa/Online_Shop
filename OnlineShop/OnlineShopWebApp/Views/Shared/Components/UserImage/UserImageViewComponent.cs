using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Views.Shared.Components.UserImage
{
    public class UserImageViewComponent : ViewComponent
    {
        private readonly UserManager<User> userManager;
        public UserImageViewComponent(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string name)
        {
            var user = await userManager.FindByNameAsync(name);
            var imagePath = user.ImagePath;
            return View("UserImage",imagePath);
        }
    }
}