using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Authorization
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}