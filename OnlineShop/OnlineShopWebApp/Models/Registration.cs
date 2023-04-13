using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Registration
    {
        [Required]
        public string Surname { get; set; }

        [Required]
        public string Name { get; set; }
        public string Fathername { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Passwordconfirm { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }
        public bool Promo { get; set; }
    }
}
