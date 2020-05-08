using SDMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Services.Interfaces
{
	public interface ILogsSerivce
	{
		public LogsModel Get(int id);
		public IList<LogsModel> GetAll(int id);
	}
}
