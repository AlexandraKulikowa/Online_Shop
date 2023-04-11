using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Order
    {
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
        public Basket Basket { get; set; }

    }
}
