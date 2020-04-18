using SDMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Services.Interfaces
{
	public interface IGroupService
	{
		public bool Create(GroupModel model);
		public GroupModel Get(int id);
		public IList<GroupModel> GetAll();
		public bool Update(GroupModel model);
		public bool Delete(int id);
	}
}
