using SDMan.Context;
using SDMan.Models;
using SDMan.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly SDManDbContext _context;
        public DepartmentService(SDManDbContext context)
        {
            _context = context;
        }
        public bool Create(DepartmentModel model)
        {

            _context.Departments.Add(model);

            return _context.SaveChanges() > 0;
        }

        public DepartmentModel Get(int id)
        {
            return _context.Departments.SingleOrDefault(b => b.Id == id);
        }

        public IList<DepartmentModel> GetAll()
        {
            return _context.Departments.ToList();
        }

        public bool Update(DepartmentModel model)
        {
            _context.Departments.Update(model);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var model = _context.Departments.SingleOrDefault(b => b.Id == id);
            if (model == null)
                return false;

            _context.Departments.Remove(model);
            return _context.SaveChanges() > 0;
        }

    }
}
