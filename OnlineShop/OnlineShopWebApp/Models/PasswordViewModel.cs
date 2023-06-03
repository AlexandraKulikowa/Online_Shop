using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class PasswordViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Укажите ваш пароль")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Ваш пароль должен быть длиной от 6 до 12 символов")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Укажите ваш новый пароль")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Ваш пароль должен быть длиной от 6 до 12 символов")]
        public string NewPassword { get; set; }

        [StringLength(12, MinimumLength = 6, ErrorMessage = "Ваш пароль должен быть длиной от 6 до 12 символов")]
        [Compare("NewPassword", ErrorMessage = "Пароль не совпадает с введенным ранее")]
        public string ConfirmNewPassword { get; set; }
    }
}
