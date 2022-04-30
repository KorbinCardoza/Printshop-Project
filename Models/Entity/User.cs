using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

#nullable disable

namespace PrintShop.Models.Entity
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        [NotMapped]
        public List<SelectListItem> RoleSelectList { get; set; }
    }
}
