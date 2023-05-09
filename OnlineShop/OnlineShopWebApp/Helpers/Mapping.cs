using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp.Helpers
{
    public static class Mapping
    {
        public static List<ProductViewModel> ToProductViewModels(List<Product> products)
        {
            var productsViewModels = new List<ProductViewModel>();
            foreach (var product in products)
            {
                productsViewModels.Add(ToProductViewModel(product));
            }
            return productsViewModels;
        }

        public static ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                Genre = (Models.Genre)product.Genre,
                PaintingTechnique = product.PaintingTechnique,
                Size = ToSizeViewModel(product.Size),
                Year = product.Year,
                IsPromo = product.IsPromo,
                ImagePath = product.ImagePath,
            };
        }

        public static SizeViewModel ToSizeViewModel(Size size)
        {
            return new SizeViewModel
            {
                Width = size.Width,
                Height = size.Height,
                IsFrame = size.IsFrame,
            };
        }

        public static Product ToProduct(ProductViewModel productVM)
        {
            return new Product
            {
                Id = productVM.Id,
                Name = productVM.Name,
                Cost = productVM.Cost,
                Description = productVM.Description,
                Genre = (OnlineShop.Db.Models.Genre)productVM.Genre,
                PaintingTechnique = productVM.PaintingTechnique,
                Size = ToSize(productVM.Size),
                Year = productVM.Year,
                IsPromo = productVM.IsPromo,
                ImagePath = productVM.ImagePath,
            };
        }

        public static Size ToSize(SizeViewModel sizeVM)
        {
            return new Size
            {
                Width = sizeVM.Width,
                Height = sizeVM.Height,
                IsFrame = sizeVM.IsFrame,
            };
        }

        public static BasketViewModel ToBasketViewModel(Basket basket)
        {
            if (basket == null || basket.BasketItems == null)
            {
                basket = new Basket();
                basket.BasketItems = new List<BasketItem>();
            }

            var basketItemsViewModels = new List<BasketItemViewModel>();

            foreach (var basketitem in basket.BasketItems)
            {
                basketItemsViewModels.Add(ToBasketItemViewModel(basketitem));
            }
            return new BasketViewModel
            {
                Id = basket.Id,
                UserId = basket.UserId,
                BasketItems = basketItemsViewModels
            };
        }

        public static BasketItemViewModel ToBasketItemViewModel(BasketItem basketItem)
        {
            return new BasketItemViewModel
            {
                Id = basketItem.Id,
                Product = Mapping.ToProductViewModel(basketItem.Product),
                Amount = basketItem.Amount,
            };
        }
    }
}