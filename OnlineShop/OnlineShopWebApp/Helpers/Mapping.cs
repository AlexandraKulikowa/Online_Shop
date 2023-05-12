using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Models;
using System;
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

        public static BasketItem ToBasketItem(BasketItemViewModel basketItemVM)
        {
            return new BasketItem
            {
                Id = basketItemVM.Id,
                Product = Mapping.ToProduct(basketItemVM.Product),
                Amount = basketItemVM.Amount,
            };
        }

        public static OrderViewModel ToOrderViewModel(Order order)
        {
            var productListVM = new List<BasketItemViewModel>();
            foreach(var basketItem in order.Products)
            {
                productListVM.Add(ToBasketItemViewModel(basketItem));
            }
            return new OrderViewModel
            {
                Id = order.Id,
                UserId = order.UserId,
                Contacts = ToContactsViewModel(order.Contacts),
                TimeFromTo = order.TimeFromTo,
                Email = order.Email,
                Mailto = order.Mailto,
                DateofDelivery = order.DateofDelivery,
                Comment = order.Comment,
                Packaging = order.Packaging,
                isAccept = order.isAccept,
                Status = order.Status,
                DateofOrder = order.DateofOrder,
                Products = productListVM
            };
        }

        public static ContactsViewModel ToContactsViewModel(Contacts contacts)
        {
            return new ContactsViewModel
            {
                Id = contacts.Id,
                Address = contacts.Address,
                Index = contacts.Index,
                Telephone = contacts.Telephone
            };
        }

        public static List<OrderViewModel> ToOrderViewModels(List<Order> orders)
        {
            var orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                orderViewModels.Add(ToOrderViewModel(order));
            }
            return orderViewModels;
        }

        public static Order ToOrder(OrderViewModel orderVM)
        {
            var productList = new List<BasketItem>();
            foreach (var basketItemVM in orderVM.Products)
            {
                productList.Add(ToBasketItem(basketItemVM));
            }

            return new Order
            {
                Id = orderVM.Id,
                UserId = orderVM.UserId,
                Contacts = ToContacts(orderVM.Contacts),
                TimeFromTo = orderVM.TimeFromTo,
                Email = orderVM.Email,
                Mailto = orderVM.Mailto,
                DateofDelivery = orderVM.DateofDelivery,
                Comment = orderVM.Comment,
                Packaging = orderVM.Packaging,
                isAccept = orderVM.isAccept,
                Status = orderVM.Status,
                DateofOrder = orderVM.DateofOrder,
                Products = productList
            };
        }

        public static Contacts ToContacts(ContactsViewModel contacts)
        {
            return new Contacts
            {
                Id = contacts.Id,
                Address = contacts.Address,
                Index = contacts.Index,
                Telephone = contacts.Telephone
            };
        }
    }
}