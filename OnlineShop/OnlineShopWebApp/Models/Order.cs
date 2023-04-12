using Microsoft.AspNetCore.Mvc;
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
        public string Address { get; set; }
        [Required]
        public string Index { get; set; }
        [Required]
        public string Telephone { get; set; }
        public string Timefrom { get; set; }
        public string Timeto { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Mailto { get; set; }
        [DataType(DataType.Date)]
        [BindProperty]
        public string Dateofdelivery { get; set; }
        public string Comment { get; set; }
        public string Packaging { get; set; }
        public bool Accept { get; set; }
        public List<BasketItem> Products { get; set; } = new List<BasketItem>();
        public decimal TotalCost()
        {
            return Products?.Sum(x => x.Cost) ?? 0;
        }

    }
}
