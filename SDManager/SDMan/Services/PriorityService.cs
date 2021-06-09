using SDMan.Context;
using SDMan.Models;
using SDMan.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Services
{
    public class PriorityService : IPriorityService
    {
        private readonly SDManDbContext _context;
        public PriorityService(SDManDbContext context)
        {
            _context = context;
        }
        public bool Create(PriorityModel model)
        {

            _context.Priorities.Add(model);

            return _context.SaveChanges() > 0;
        }
        public PriorityModel Get(int id)
        {
            return _context.Priorities.SingleOrDefault(b => b.Id == id);
        }
        public IList<PriorityModel> GetAll()
        {
            return _context.Priorities.ToList();
        }
        public bool Update(PriorityModel model)
        {
            _context.Priorities.Update(model);
            return _context.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            var model = _context.Priorities.SingleOrDefault(b => b.Id == id);
            if (model == null)
                return false;

            _context.Priorities.Remove(model);
            return _context.SaveChanges() > 0;
        }
    }
}
