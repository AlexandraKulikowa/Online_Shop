using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Не задано название роли")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Напишите корректное название")]
        public string Name { get; set; }

    }
}