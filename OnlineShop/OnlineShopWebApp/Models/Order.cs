using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class Order
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        [Required]
        public Contacts Contacts { get; set; }
        public string Timefrom { get; set; }
        public string Timeto { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Mailto { get; set; }
        public string DateofDelivery { get; set; }
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