using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class RoleViewModel
    {

        [Required(ErrorMessage = "Не задано название роли")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Напишите корректное название")]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var role = (RoleViewModel)obj;
            return Name == role.Name;
        }

        public override int GetHashCode()
        {
            return 123456;
        }
    }
}