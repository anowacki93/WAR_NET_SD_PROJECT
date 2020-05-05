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

namespace SDMan.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SDManDbContext _context;
        IncidentModel testModel = new IncidentModel();
        public IncidentService(SDManDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public bool Create(IncidentModel incidentModel)
        {

            
            incidentModel.LastModified = DateTime.UtcNow.AddHours(2);
            incidentModel.CreatedDate = DateTime.UtcNow.AddHours(2);
            incidentModel.Status = _context.Statuses.Where(x=>x.Name=="Open").FirstOrDefault();
            incidentModel.StatusName = incidentModel.Status.Name;
            incidentModel.RoleName = _context.Roles.Where(x => x.Name == "Helpdesk").Select(x => x.Name).FirstOrDefault();
            incidentModel.Category = _context.Categories.Where(x => x.Name == incidentModel.CategoryName).FirstOrDefault();
            incidentModel.Priority = _context.Priorities.Where(x => x.Name == incidentModel.PriorityName).FirstOrDefault();
            incidentModel.Department = _context.Departments.Where(x => x.Name == incidentModel.DepartmentName).FirstOrDefault();
            
            _context.Incidents.Add(incidentModel);
            return _context.SaveChanges() > 0;
        }

        public IncidentModel Get(int id)
        {
            var incidentModel = new IncidentModel();
            incidentModel = _context.Incidents.SingleOrDefault(b => b.Id == id);
            incidentModel.Category = _context.Categories.Where(x => x.Name == incidentModel.CategoryName).FirstOrDefault();
            incidentModel.Priority = _context.Priorities.Where(x => x.Name == incidentModel.PriorityName).FirstOrDefault();
            incidentModel.Department = _context.Departments.Where(x => x.Name == incidentModel.DepartmentName).FirstOrDefault();
            incidentModel.Status = _context.Statuses.Where(x => x.Name == incidentModel.StatusName).FirstOrDefault();
            return incidentModel;
        }

        public IList<IncidentModel> GetAll()
        {
            
            return _context.Incidents.ToList();
        }

        public bool Update(IncidentModel model)
        {
            model.CreatedDate = model.CreatedDate;
            model.Category = _context.Categories.Where(x => x.Name == model.CategoryName).FirstOrDefault();
            model.Priority = _context.Priorities.Where(x => x.Name == model.PriorityName).FirstOrDefault();
            model.Department = _context.Departments.Where(x => x.Name == model.DepartmentName).FirstOrDefault();
            model.Status = _context.Statuses.Where(x => x.Name == model.StatusName).FirstOrDefault();
            _context.Incidents.Update(model);
            //_context.Incidents.Add(model);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var model = _context.Incidents.SingleOrDefault(b => b.Id == id);
            if (model == null)
                return false;

            _context.Incidents.Remove(model);
            return _context.SaveChanges() > 0;
        }
    }
}
