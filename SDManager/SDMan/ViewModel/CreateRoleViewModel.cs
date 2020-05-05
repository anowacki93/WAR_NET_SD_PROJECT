using Microsoft.AspNetCore.Identity;
using SDMan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.ViewModel
{
    public class CreateRoleViewModel
    {
        public string RoleName { get; set; }

    }
}
