using SDMan.Context;
using SDMan.Models;
using SDMan.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Services
{
    public class StatusService : IStatusService
    {
        private readonly SDManDbContext _context;
        public StatusService(SDManDbContext context)
        {
            _context = context;
        }
        public bool Create(StatusModel model)
        {
            _context.Statuses.Add(model);
            return _context.SaveChanges() > 0;
        }
        public StatusModel Get(int id)
        {
            return _context.Statuses.SingleOrDefault(b => b.Id == id);
        }
        public IList<StatusModel> GetAll()
        {
            return _context.Statuses.ToList();
        }
        public bool Update(StatusModel model)
        {
            _context.Statuses.Update(model);
            return _context.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            var model = _context.Statuses.SingleOrDefault(b => b.Id == id);
            if (model == null)
                return false;
            _context.Statuses.Remove(model);
            return _context.SaveChanges() > 0;
        }
    }
}
