using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Role
    {
        private static int counter = 1;
        public int Id { get; set; }

        [Required(ErrorMessage = "Не задано название роли")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Напишите корректное название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не описаны функции роли")]
        [StringLength(70, MinimumLength = 10, ErrorMessage = "Напишите функции роли")]
        public string Options { get; set; }

        public Role()
        {
            Id = counter;
            counter++;
        }
        public Role(string name, string options) : this()
        {
            Name = name;
            Options = options;
        }
    }
}
