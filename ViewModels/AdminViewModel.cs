using System.Collections.Generic;
using PrintShop.Models.Entity;
namespace PrintShop.ViewModels
{
    public class AdminViewModel
    {
        public List<User> UserList { get; set; }
        public List<Product> ProductList { get; set; }
        public Models.Entity.System System { get; set; }
    }
}
