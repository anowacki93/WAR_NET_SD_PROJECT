using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SDMan.Context;
using SDMan.Models;
using SDMan.Services.Interfaces;

namespace SDMan.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _modelService;
        public CategoryController(ICategoryService modelService)
        {
            _modelService = modelService;
        }
        public IActionResult Index()
        {   
            var modelData = _modelService.GetAll();
            return View(modelData);
        }
        public IActionResult Create()
        {

            CategoryModel model = new CategoryModel();
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Create([FromForm]CategoryModel model) //[FromForm] - potrzebne do mapowania danych z formularza na obiekt w przypadku gdy korzystamy z Razora lub zwykłego HTMLa. Przy taghelperach nie jest to konieczne tak jak w tym przypadku
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _modelService.Create(model);
                    //context.SaveChanges();
                }
                catch (Exception)
                {
                    return View(model);
                }
                return Redirect("Index");
            }
            else
            {
                return View(model);
            }
        }
        public IActionResult Delete(int id)
        {
            return View(_modelService.Get(id));
        }
        [HttpPost]
        public IActionResult RemoveConfirm(int id)
        {
            try
            {
                _modelService.Delete(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
                throw;
            }
            return Redirect("Index");
        }
        public IActionResult Edit(int id)
        {
            return View(_modelService.Get(id));
        }
        [HttpPost]
        public IActionResult EditSave(CategoryModel model)
        {
            try
            {

                _modelService.Update(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
                throw;
            }
            return Redirect("Index");
        }
        public IActionResult Details(int id)
        {
            return View(_modelService.Get(id));
        }
        public IActionResult Show()
        {
            var modelData = _modelService.GetAll();
            return View(modelData.Count());
        }
    }
}
