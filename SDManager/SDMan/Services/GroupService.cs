using SDMan.Context;
using SDMan.Models;
using SDMan.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Services
{
    public class GroupService : IGroupService
    {
        private readonly SDManDbContext _context;
        public GroupService(SDManDbContext context)
        {
            _context = context;
        }
        public bool Create(GroupModel model)
        {

            _context.Groups.Add(model);

            return _context.SaveChanges() > 0;
        }

        public GroupModel Get(int id)
        {
            return _context.Groups.SingleOrDefault(b => b.Id == id);
        }

        public IList<GroupModel> GetAll()
        {
            return _context.Groups.ToList();
        }

        public bool Update(GroupModel model)
        {
            _context.Groups.Update(model);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var model = _context.Groups.SingleOrDefault(b => b.Id == id);
            if (model == null)
                return false;

            _context.Groups.Remove(model);
            return _context.SaveChanges() > 0;
        }

    }
}
