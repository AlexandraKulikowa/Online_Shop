using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Models
{
    public class User : IdentityUser
    {
        public string? Surname { get; set; } = string.Empty;
        public string? Fathername { get; set; } = string.Empty;
        public bool? isDistribution { get; set; } = false;
    }
}
