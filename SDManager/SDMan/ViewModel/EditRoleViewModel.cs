using Microsoft.AspNetCore.Identity;
using SDMan.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDMan.ViewModel
{
    
        public class EditRoleViewModel
        {
            public EditRoleViewModel()
            {
                Users = new List<string>();
            }

            public int Id { get; set; }

            [Required(ErrorMessage = "Role Name is required")]
            public string Rolename { get; set; }

            public List<string> Users { get; set; }
        }
}
