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

        public string CreateImage(UserViewModel userVM)
        {
            if (userVM.UploadedFile != null)
            {
                var imagesPath = Path.Combine(appEnvironment.WebRootPath + "/images/users/");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }
                var fileName = Guid.NewGuid() + "." + userVM.UploadedFile.FileName.Split('.').Last();
                using (var fileStream = new FileStream(imagesPath + fileName, FileMode.Create))
                {
                    userVM.UploadedFile.CopyTo(fileStream);
                }
                userVM.ImagePath = "/images/users/" + fileName;
            }
            return userVM.ImagePath;
        }
}
}