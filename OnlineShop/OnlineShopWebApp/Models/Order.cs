using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class Order
    {
        public string UserId { get; set; }

        public int Id { get; set; }

        public Contacts Contacts { get; set; }

        public string TimeFromTo { get; set; }

        [Required(ErrorMessage ="Вы не ввели e-mail")]
        [EmailAddress(ErrorMessage = "Некорректный e-mail!")]
        public string Email { get; set; }

        public string Mailto { get; set; }

        [Required(ErrorMessage = "Вы не ввели удобную вам дату доставки!")]
        [Remote(action: "CheckDate", controller: "Order", HttpMethod ="POST", ErrorMessage = "Это дата из прошлого, а в прошлое вернуться нельзя!")]
        public DateTime DateofDelivery { get; set; }

        public string Comment { get; set; }

        public PackagingEnum Packaging { get; set; }

        public bool isAccept { get; set; }

        public StatusEnum Status { get; set; } = StatusEnum.Open;

        public List<BasketItem> Products { get; set; } = new List<BasketItem>();

        public decimal TotalCost()
        {
            return Products?.Sum(x => x.Cost) ?? 0;
        }
    }
}