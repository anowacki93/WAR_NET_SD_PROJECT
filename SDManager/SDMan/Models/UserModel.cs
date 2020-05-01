using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Models
{
    public class UserModel :IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
        //public int GroupId { get; set; }
        public IdentityRole Role { get; set; }
        public int? GroupId { get; set; }
        public GroupModel? Group { get; set; }
        

    }
}
