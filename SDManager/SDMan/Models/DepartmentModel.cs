using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Models
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<IncidentModel> Incidents { get; set; }
    }
}
