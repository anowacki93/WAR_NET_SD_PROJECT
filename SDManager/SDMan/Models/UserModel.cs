using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Models
{
    public class UserModel :IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
        public int RoleId { get; set; }
        public IdentityRole Role { get; set; }
        public int GroupId { get; set; }
        public GroupModel Group { get; set; }
    }
}
