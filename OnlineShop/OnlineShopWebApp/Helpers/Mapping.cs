using Microsoft.AspNetCore.Identity;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Helpers
{
    public static class Mapping
    {
        public static List<ProductViewModel> ToProductViewModels(this List<Product> products)
        {
            var productsViewModels = new List<ProductViewModel>();
            productsViewModels = products.Select(x => x.ToProductViewModel()).ToList();

            return productsViewModels;
        }

        public static ProductViewModel ToProductViewModel(this Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                Genre = (Models.Genre)product.Genre,
                PaintingTechnique = product.PaintingTechnique,
                Size = product.Size.ToSizeViewModel(),
                Year = product.Year,
                IsPromo = product.IsPromo,
                ImagePath = product.ImagePath,
            };
        }

        public static SizeViewModel ToSizeViewModel(this Size size)
        {
            return new SizeViewModel
            {
                Width = size.Width,
                Height = size.Height,
                IsFrame = size.IsFrame,
            };
        }

        public static Product ToProduct(this ProductViewModel productVM)
        {
            return new Product
            {
                Id = productVM.Id,
                Name = productVM.Name,
                Cost = productVM.Cost,
                Description = productVM.Description,
                Genre = (OnlineShop.Db.Models.Genre)productVM.Genre,
                PaintingTechnique = productVM.PaintingTechnique,
                Size = productVM.Size.ToSize(),
                Year = productVM.Year,
                IsPromo = productVM.IsPromo,
                ImagePath = productVM.ImagePath,
            };
        }

        public static Size ToSize(this SizeViewModel sizeVM)
        {
            return new Size
            {
                Width = sizeVM.Width,
                Height = sizeVM.Height,
                IsFrame = sizeVM.IsFrame,
            };
        }

        public static BasketViewModel ToBasketViewModel(this Basket basket)
        {
            if (basket == null || basket.BasketItems == null)
            {
                basket = new Basket();
                basket.BasketItems = new List<BasketItem>();
            }

            var basketItemsViewModels = new List<BasketItemViewModel>();            
            basketItemsViewModels = basket.BasketItems.Select(x => x.ToBasketItemViewModel()).ToList();

            return new BasketViewModel
            {
                Id = basket.Id,
                UserId = basket.UserId,
                BasketItems = basketItemsViewModels
            };
        }

        public static BasketItemViewModel ToBasketItemViewModel(this BasketItem basketItem)
        {
            return new BasketItemViewModel
            {
                Id = basketItem.Id,
                Product = basketItem.Product.ToProductViewModel(),
                Amount = basketItem.Amount,
            };
        }

        public static BasketItem ToBasketItem(this BasketItemViewModel basketItemVM)
        {
            return new BasketItem
            {
                Id = basketItemVM.Id,
                Product = basketItemVM.Product.ToProduct(),
                Amount = basketItemVM.Amount
            };
        }

        public static OrderViewModel ToOrderViewModel(this Order order)
        {
            var productListVM = new List<BasketItemViewModel>();
            productListVM = order.OrderBasketItems.Select(x => x.ToBasketItemViewModel()).ToList();

            return new OrderViewModel
            {
                Id = order.Id,
                UserId = order.UserId,
                Contacts = order.Contacts.ToContactsViewModel(),
                TimeFromTo = order.TimeFromTo,
                Email = order.Email,
                Mailto = order.Mailto,
                DateofDelivery = order.DateofDelivery,
                Comment = order.Comment,
                Packaging = order.Packaging,
                isAccept = order.IsAccept,
                Status = order.Status,
                DateofOrder = order.DateofOrder,
                Products = productListVM
            };
        }

        public static ContactsViewModel ToContactsViewModel(this Contacts contacts)
        {
            return new ContactsViewModel
            {
                Id = contacts.Id,
                Address = contacts.Address,
                Index = contacts.Index,
                Telephone = contacts.Telephone
            };
        }

        public static List<OrderViewModel> ToOrderViewModels(this List<Order> orders)
        {
            var orderViewModels = new List<OrderViewModel>();
            orderViewModels = orders.Select(x => x.ToOrderViewModel()).ToList();

            return orderViewModels;
        }

        public static Order ToOrder(this OrderViewModel orderVM)
        {
            var productList = new List<BasketItem>();
            productList = orderVM.Products.Select(x => x.ToBasketItem()).ToList();

            return new Order
            {
                UserId = orderVM.UserId,
                Contacts = orderVM.Contacts.ToContacts(),
                TimeFromTo = orderVM.TimeFromTo,
                Email = orderVM.Email,
                Mailto = orderVM.Mailto,
                DateofDelivery = orderVM.DateofDelivery,
                Comment = orderVM.Comment,
                Packaging = orderVM.Packaging,
                IsAccept = orderVM.isAccept,
                Status = orderVM.Status,
                DateofOrder = orderVM.DateofOrder,
                OrderBasketItems = productList
            };
        }

        public static Contacts ToContacts(this ContactsViewModel contacts)
        {
            return new Contacts
            {
                Id = contacts.Id,
                Address = contacts.Address,
                Index = contacts.Index,
                Telephone = contacts.Telephone
            };
        }
        public static User ToUser(this RegistrationViewModel registration)
        {
            return new User
            {
                Surname = registration.Surname,
                Name = registration.Name,
                Fathername = registration.Fathername,
                UserName = registration.Login,
                Email = registration.Email,
                PhoneNumber = registration.Phone,
                isDistribution = registration.isDistribution
            };
        }

        public static UserViewModel ToUserViewModel(this User user)
        {

            return new UserViewModel
            {
                Id = user.Id,
                Surname = user.Surname,
                Name = user.Name,
                Fathername = user.Fathername,
                Login = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                isDistribution = user.isDistribution
            };
        }

        public static List<UserViewModel> ToUserViewModels(this List<User> users)
        {
            var userViewModels = new List<UserViewModel>();
            userViewModels = users.Select(x => x.ToUserViewModel()).ToList();

            return userViewModels;
        }

        public static void ChangeUser(this User user, UserViewModel userVM)
        {
            user.Surname = userVM.Surname;
            user.Name = userVM.Name;
            user.Fathername = userVM.Fathername;
            user.UserName = userVM.Login;
            user.Email = userVM.Email;
            user.PhoneNumber = userVM.Phone;
            user.isDistribution = userVM.isDistribution;
        }

        public static RoleViewModel ToRoleViewModel(this IdentityRole role)
        {
            return new RoleViewModel { Id = role.Id, Name = role.Name };
        }
    }
}