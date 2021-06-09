using SDMan.Context;
using SDMan.Models;
using SDMan.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly SDManDbContext _context;
        public CategoryService(SDManDbContext context)
        {
            _context = context;
        }
        public bool Create(CategoryModel model)
        {

            _context.Categories.Add(model);

            return _context.SaveChanges() > 0;
        }
        public CategoryModel Get(int id)
        {
            return _context.Categories.SingleOrDefault(b => b.Id == id);
        }
        public IList<CategoryModel> GetAll()
        {
            return _context.Categories.ToList();
        }
        public bool Update(CategoryModel model)
        {
            _context.Categories.Update(model);
            return _context.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            var model = _context.Categories.SingleOrDefault(b => b.Id == id);
            if (model == null)
                return false;

            _context.Categories.Remove(model);
            return _context.SaveChanges() > 0;
        }     
    }
}
