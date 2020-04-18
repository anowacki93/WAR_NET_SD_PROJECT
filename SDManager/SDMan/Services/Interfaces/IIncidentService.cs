using SDMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Services.Interfaces
{
	public interface IIncidentService
	{
		public bool Create(IncidentModel model);
		public IncidentModel Get(int id);
		public IList<IncidentModel> GetAll();
		public bool Update(IncidentModel model);
		public bool Delete(int id);
	}
}
