using SDMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Services.Interfaces
{
    public interface IPriorityService
    {
        public bool Create(PriorityModel model);
        public PriorityModel Get(int id);
        public IList<PriorityModel> GetAll();
        public bool Update(PriorityModel model);
        public bool Delete(int id);
    }
}
