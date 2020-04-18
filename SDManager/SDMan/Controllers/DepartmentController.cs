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
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _modelService;
        public DepartmentController(IDepartmentService modelService)
        {
            _modelService = modelService;
        }
        public IActionResult Index()
        {   //Po utworzeniu userów
            //var user = User.Identity.Name;
            //var modelData = _modelService.GetAll().Where(x => x.UserId == user);
            //Przed utworzeniem userow
            var modelData = _modelService.GetAll();
            return View(modelData);
        }
        public IActionResult Create()
        {
            DepartmentModel model = new DepartmentModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create([FromForm]DepartmentModel model) //[FromForm] - potrzebne do mapowania danych z formularza na obiekt w przypadku gdy korzystamy z Razora lub zwykłego HTMLa. Przy taghelperach nie jest to konieczne tak jak w tym przypadku
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //model.UserId = User.Identity.Name;

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
        public IActionResult Remove(int id)
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
        public IActionResult EditSave(DepartmentModel model)
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
