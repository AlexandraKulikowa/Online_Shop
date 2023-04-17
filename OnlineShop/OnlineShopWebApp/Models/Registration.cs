using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Registration
    {
        [Required(ErrorMessage = "Укажите вашу фамилию")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Ваша фамилия должна быть длиной от 1 до 25 символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Укажите ваше имя")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Ваше имя должно быть длиной от 2 до 25 символов")]
        public string Name { get; set; }

        [StringLength(25, MinimumLength = 5, ErrorMessage = "Ваше отчество должно быть длиной от 5 до 25 символов")]
        public string Fathername { get; set; }

        [Required(ErrorMessage = "Укажите ваш логин")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Ваш логин должен быть длиной от 5 до 25 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Укажите ваш пароль")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Ваш пароль должен быть длиной от 6 до 12 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Укажите пароль повторно")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Ваш пароль должен быть длиной от 6 до 12 символов")]
        [Compare("Password", ErrorMessage ="Пароль не совпадает с введенным выше")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Укажите ваш e-mail")]
        [EmailAddress(ErrorMessage ="Некорректный e-mail!")]
        public string Email { get; set; }

        [Phone]
        [Required(ErrorMessage = "Укажите ваш номер телефона")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{10}$", ErrorMessage = "Укажите верный номер телефона")]
        public string Phone { get; set; }

        public bool Promo { get; set; }
    }
}
