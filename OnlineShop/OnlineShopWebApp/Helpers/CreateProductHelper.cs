using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Collections.Generic;

namespace OnlineShopWebApp.Helpers
{
    public class CreateProductHelper
    {
        private readonly IWebHostEnvironment appEnvironment;
        public CreateProductHelper(IWebHostEnvironment appEnvironment)
        {
            this.appEnvironment = appEnvironment;
        }

        public Product CreateProduct(ProductViewModel productVM)
        {
            productVM.ImagePath = new List<string>();
            if (productVM.UploadedFiles != null)
            {
                var ImagesPath = Path.Combine(appEnvironment.WebRootPath + "/images/products/");
                if (!Directory.Exists(ImagesPath))
                {
                    Directory.CreateDirectory(ImagesPath);
                }

                foreach (var file in productVM.UploadedFiles)
                {
                    var fileName = Guid.NewGuid() + "." + file.FileName.Split('.').Last();
                    using (var fileStream = new FileStream(ImagesPath + fileName, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.ImagePath.Add("/images/products/" + fileName);
                }
            }
            return productVM.ToProduct();
        }
    }
}