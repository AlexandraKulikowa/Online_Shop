using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Models
{
    public class User : IdentityUser
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Fathername { get; set; }
        public bool isDistribution { get; set; }
        public string ImagePath { get; set; }
    }
}