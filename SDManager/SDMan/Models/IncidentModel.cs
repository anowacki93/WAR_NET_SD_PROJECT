using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Models
{
    public class IncidentModel
    {
        public int Id { get; set; }
        public string IncidentDescription { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public CategoryModel Category { get; set; }
        public string DepartmentName { get; set; }
        public DepartmentModel Department{ get; set; }
        public string StatusName { get; set; }
        public StatusModel Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}")]
        public DateTime CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}")]
        public DateTime LastModified { get; set; }
        public UserModel? Assignee { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string PriorityName { get; set; }
        public PriorityModel Priority { get; set; }
        [NotMapped]

        public SelectList? ListPriorities { get; set; }
        [NotMapped]

        public List<CategoryModel> ListCategories { get; set; }
        [NotMapped]

        public SelectList? ListDepartments { get; set; }

    }
}
