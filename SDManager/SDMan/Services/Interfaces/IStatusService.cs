using SDMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Services.Interfaces
{
    public interface IStatusService
    {public bool Create(StatusModel model);
		public StatusModel Get(int id);
		public IList<StatusModel> GetAll();
		public bool Update(StatusModel model);
		public bool Delete(int id);
    }
}
