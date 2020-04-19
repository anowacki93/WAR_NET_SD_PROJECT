using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SDMan.Context;
using SDMan.Models;
using SDMan.Services.Interfaces;

namespace SDMan.Controllers
{
    public class IncidentController : Controller
    {
        private readonly SignInManager<UserModel> signInManager;
        private readonly IIncidentService _modelService;
        private readonly IPriorityService _priorityService;
        private readonly ICategoryService _categoryService;
        private readonly IDepartmentService _departmentService;
        private readonly SDManDbContext _context;
        public IncidentController(IIncidentService modelService, IDepartmentService departmentService, ICategoryService categoryService, IPriorityService priorityService)
        {
            _priorityService = priorityService;
            _categoryService = categoryService;
            _departmentService = departmentService;
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
            IncidentModel model = new IncidentModel();
            //model.ListPriorites = new SelectList(_modelService.GetAll(), model.Priority);
            //model.ListPriorites = new SelectList(_modelService.GetAll().ToList());
            model.ListPriorities = new SelectList(_priorityService.GetAll().Select(x => x.Name).ToList(), model.PriorityName);
            model.ListDepartments = new SelectList(_departmentService.GetAll().Select(x => x.Name).ToList(), model.DepartmentName);
            model.ListCategories = new SelectList(_categoryService.GetAll().Select(x => x.Name).ToList(), model.CategoryName);
            //model.ListPriorities = _priorityService.GetAll().ToList();
            return View(model);
        }
      
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]IncidentModel model) //[FromForm] - potrzebne do mapowania danych z formularza na obiekt w przypadku gdy korzystamy z Razora lub zwykłego HTMLa. Przy taghelperach nie jest to konieczne tak jak w tym przypadku
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var user = await signInManager.UserManager.FindByIdAsync(User.Identity.ToString());
                    //model.CreatedBy = model.ModifiedBy = user;
                    //model.UserId = User.Identity.Name;

                    // model.ListPriorites = new SelectList(_modelService.GetAll());
                    
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
        public IActionResult Edit(IncidentModel model,int id)
        {

            return View(_modelService.Get(id));
        }
        [HttpPost]
        public IActionResult EditSave(IncidentModel model)
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
