using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{

    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Укажите вашу фамилию")]
        [StringLength(26, MinimumLength = 2, ErrorMessage = "Ваша фамилия должна быть длиной от 2 до 26 символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Укажите ваше имя")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Ваше имя должно быть длиной от 2 до 25 символов")]
        public string Name { get; set; }

        [StringLength(25, MinimumLength = 5, ErrorMessage = "Ваше отчество должно быть длиной от 5 до 25 символов")]
        public string Fathername { get; set; }

        [Required(ErrorMessage = "Укажите ваш логин")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Ваш логин должен быть длиной от 5 до 25 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Укажите ваш e-mail")]
        [StringLength(28, MinimumLength = 6, ErrorMessage = "Это подозрительно длинный e-mail, проверьте правильность написания!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [EmailAddress(ErrorMessage = "Некорректный e-mail!")]
        public string Email { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Укажите ваш номер телефона")]
        [RegularExpression(@"^((\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{10}$", ErrorMessage = "Укажите верный номер телефона")]
        public string Phone { get; set; }

        public bool isDistribution { get; set; }

        public List<string>? Roles { get; set; }
    }
}
