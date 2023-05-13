using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public Contacts Contacts { get; set; }

        public string TimeFromTo { get; set; }

        public string Email { get; set; }

        public string Mailto { get; set; }

        public DateTime DateofDelivery { get; set; }

        public string Comment { get; set; }

        public Packaging Packaging { get; set; }

        public bool isAccept { get; set; }

        public Status Status { get; set; } = Status.Открыт;

        public DateTime DateofOrder { get; set; } = DateTime.Now;

        public List<BasketItem> OrderBasketItems { get; set; } 

        public Order()
        {
            OrderBasketItems = new List<BasketItem>();
        }
    }
}