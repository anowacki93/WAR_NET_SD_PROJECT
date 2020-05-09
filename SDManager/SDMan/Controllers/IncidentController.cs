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
        private readonly UserManager<UserModel> _userManager;
        private readonly IIncidentService _modelService;
        private readonly IPriorityService _priorityService;
        private readonly ICategoryService _categoryService;
        private readonly IDepartmentService _departmentService;
        private readonly SDManDbContext _context;
        IncidentModel _modelTest = new IncidentModel();
        public IncidentController(IIncidentService modelService, IDepartmentService departmentService, ICategoryService categoryService, IPriorityService priorityService, SDManDbContext context, UserManager<UserModel> userManager)
        {
            _priorityService = priorityService;
            _categoryService = categoryService;
            _departmentService = departmentService;
            _modelService = modelService;
            _context = context;
            _userManager = userManager;
            
        }
        public IActionResult Index()
        {  
            var modelData = _modelService.GetAll();
            if (User.IsInRole("Administrator"))
            {
                modelData = _modelService.GetAll();
            }
            else
            {
                modelData = _modelService.GetAll().Where(x => x.CreatedBy == User.Identity.Name).ToList();
            }
            

            return View(modelData);
        }
        
        public IActionResult GroupIncidents()
        {
            var modelData = _modelService.GetAll().Where(x => x.RoleName == "IT");
            if (User.IsInRole("Systems"))
            {
              modelData = _modelService.GetAll().Where(x => x.RoleName == "Systems");
            }
            else
            {
                modelData = _modelService.GetAll().Where(x => x.RoleName == "Helpdesk");
            }

            return View(modelData);
        }

        public IActionResult MyIncidents()
        {
            var modelData = _modelService.GetAll();

            modelData = _modelService.GetAll().Where(x => x.AssigneeName == User.Identity.Name).ToList();
            return View(modelData);
        }
        public IActionResult Create()
        {
            IncidentModel model = new IncidentModel();
            return View(model);
        }
      
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]IncidentModel model,int id) //[FromForm] - potrzebne do mapowania danych z formularza na obiekt w przypadku gdy korzystamy z Razora lub zwykłego HTMLa. Przy taghelperach nie jest to konieczne tak jak w tym przypadku
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreatedBy = User.Identity.Name;
                    model.ModifiedBy = User.Identity.Name;
                    var test =  model.CreatedBy;
                    _modelService.Create(model);
                    model.LogId = model.Logs.Id;
                    _modelService.Update(model);
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
        public async Task<IActionResult> Edit(int id)
        {            
            return View(_modelService.Get(id));
        }
        [HttpPost]
        public IActionResult EditSave(IncidentModel model)
        {            
            try
            {
               
                model.LastModified = DateTime.UtcNow.AddHours(2);
                model.ModifiedBy = User.Identity.Name;
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
        [HttpPost]
        public JsonResult CategoryJS(string prefix)
        {
            List<CategoryModel> list = new List<CategoryModel>();
            list = _context.Categories.ToList();
            var CatList = (from N in list
                            where N.Name.StartsWith(prefix)
                            select new { value = N.Name,label = N.Name });
            return Json(CatList);
        }
        
        [HttpPost]
        public JsonResult DepartmentJS(string prefix)
        {
            List<DepartmentModel> list = new List<DepartmentModel>();
            list = _context.Departments.ToList();
            var GList = (from N in list
                         where N.Name.StartsWith(prefix)
                         select new { value = N.Name, label = N.Name });
            return Json(GList);
        }
        [HttpPost]
        public JsonResult PriorityJS(string prefix)
        {
            List<PriorityModel> list = new List<PriorityModel>();
            list = _context.Priorities.ToList();
            var GList = (from N in list
                         where N.Name.StartsWith(prefix)
                         select new { value = N.Name, label = N.Name });
            return Json(GList);
        }
        [HttpPost]
        public JsonResult StatusJS(string prefix)
        {
            List<StatusModel> list = new List<StatusModel>();
            list = _context.Statuses.ToList();
            var GList = (from N in list
                         where N.Name.StartsWith(prefix)
                         select new { value = N.Name, label = N.Name });
            return Json(GList);
        }
        public JsonResult AssigneeJS(string prefix)
        {
            List<UserModel> list = new List<UserModel>();
            list = _context.Users.ToList();
            var GList = (from N in list
                         where N.UserName.StartsWith(prefix)
                         select new { value = N.UserName, label = N.UserName });
            return Json(GList);
        }
        public JsonResult RoleJS(string prefix)
        {
            List<IdentityRole<int>> list = new List<IdentityRole<int>>();
            list = _context.Roles.ToList();
            var GList = (from N in list
                         where N.Name.StartsWith(prefix)
                         select new { value = N.Name, label = N.Name });
            return Json(GList);
        }
        

    }
}
