using Microsoft.AspNetCore.Hosting;
using OnlineShopWebApp.Models;
using System.IO;
using System;
using System.Linq;

namespace OnlineShopWebApp.Helpers
{
    public class CreateUserImage
    {
        private readonly IWebHostEnvironment appEnvironment;

        public CreateUserImage(IWebHostEnvironment appEnvironment)
        {
            this.appEnvironment = appEnvironment;
        }

        public string CreateImage(UserViewModel user)
        {
            if (user.UploadedFile != null)
            {
                var imagesPath = Path.Combine(appEnvironment.WebRootPath + "/images/users/");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }
                var fileName = Guid.NewGuid() + "." + user.UploadedFile.FileName.Split('.').Last();
                using (var fileStream = new FileStream(imagesPath + fileName, FileMode.Create))
                {
                    user.UploadedFile.CopyTo(fileStream);
                }
                user.ImagePath = "/images/users/" + fileName;
            }
            return user.ImagePath;
        }
}
}