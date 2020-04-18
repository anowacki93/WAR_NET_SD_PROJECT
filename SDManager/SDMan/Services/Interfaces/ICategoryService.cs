using SDMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Services.Interfaces
{
    public interface ICategoryService
	{
		public bool Create(CategoryModel model);
		public CategoryModel Get(int id);
		public IList<CategoryModel> GetAll();
		public bool Update(CategoryModel model);
		public bool Delete(int id);
	}
}
