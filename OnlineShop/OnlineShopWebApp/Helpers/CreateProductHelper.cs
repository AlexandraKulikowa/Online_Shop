using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace OnlineShopWebApp.Helpers
{
    public class CreateProductHelper
    {
        private readonly IWebHostEnvironment appEnvironment;
        public CreateProductHelper(IWebHostEnvironment appEnvironment)
        {
            this.appEnvironment = appEnvironment;
        }

        public Product CreateProduct(ProductViewModel product)
        {
            if (product.UploadedFile != null)
            {
                var imagesPath = Path.Combine(appEnvironment.WebRootPath + "/images/products");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }
                var fileName = Guid.NewGuid() + "." + product.UploadedFile.FileName.Split('.').Last();
                using (var fileStream = new FileStream(imagesPath + fileName, FileMode.Create))
                {
                    product.UploadedFile.CopyTo(fileStream);
                }
                product.ImagePath = "/images/products" + fileName;
            }

            return product.ToProduct();
        }
    }
}