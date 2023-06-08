using System.Collections.Generic;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class RightsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List <RoleViewModel> UserRoles { get; set; }
        public List<RoleViewModel> AllRoles { get; set; }
    }
}
