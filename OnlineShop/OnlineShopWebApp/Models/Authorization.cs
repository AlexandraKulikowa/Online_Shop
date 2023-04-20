using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Authorization
    {
        [Required(ErrorMessage = "Укажите ваш логин")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Ваш логин должен быть длиной от 5 до 25 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Укажите ваш пароль")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Ваш пароль должен быть длиной от 6 до 12 символов")]
        public string Password { get; set; }

        public bool IsRemember { get; set; }
    }
}