using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Models
{
    public class IncidentModel
    {
        public int Id { get; set; }
        public string IncidentDescription { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentModel Departments{ get; set; }
        public int StatusId { get; set; }
        public StatusModel Status { get; set; }
        public DateTime CreatedDate { set { CreatedDate = DateTime.UtcNow.AddHours(1); } }
        public DateTime LastModified { get; set; }
        public UserModel Assignee { get; set; }
        public UserModel CreatedBy { get; set; }
        public UserModel ModifiedBy { get; set; }

    }
}
