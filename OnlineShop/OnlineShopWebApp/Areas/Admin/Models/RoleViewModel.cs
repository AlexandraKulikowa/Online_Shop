using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class RoleViewModel
    {

        [Required(ErrorMessage = "Не задано название роли")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Название должно быть длиной от 4 до 20 символов")]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var role = (RoleViewModel)obj;
            return Name == role.Name;
        }
    }
}