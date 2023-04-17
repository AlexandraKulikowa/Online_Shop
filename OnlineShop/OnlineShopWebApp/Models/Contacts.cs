using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Contacts
    {
        [Required(ErrorMessage ="Вы не ввели адрес доставки")]
        [StringLength(190,MinimumLength = 10, ErrorMessage ="Введите верный адрес")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Вы не ввели индекс доставки")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Введите индекс в верном формате")]
        public int Index { get; set; }

        [Required(ErrorMessage = "Вы не ввели телефон")]
        [Phone(ErrorMessage ="Введите телефон в верном формате")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{10}$", ErrorMessage = "Укажите верный номер телефона")]
        public string Telephone { get; set; }
    }
}