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
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentModel Department{ get; set; }
        public int StatusId { get; set; }
        public StatusModel Status { get; set; }
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LastModified { get; set; }
        public UserModel? Assignee { get; set; }
        public UserModel CreatedBy { get; set; }
        public UserModel ModifiedBy { get; set; }
        public int PriorityId { get; set; }
        public PriorityModel Priority { get; set; }
        [NotMapped]

        public SelectList? ListPriorites { get; set; }

    }
}
