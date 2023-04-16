using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Contacts
    {
        public string Address { get; set; }
        [Required]
        public string Index { get; set; }
        [Required]
        public string Telephone { get; set; }
    }
}