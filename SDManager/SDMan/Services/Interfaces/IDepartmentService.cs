using SDMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Services.Interfaces
{
	public interface IDepartmentService
	{
		public bool Create(DepartmentModel model);
		public DepartmentModel Get(int id);
		public IList<DepartmentModel> GetAll();
		public bool Update(DepartmentModel model);
		public bool Delete(int id);
	}
}
