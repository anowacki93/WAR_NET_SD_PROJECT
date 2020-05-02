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
            //incidentModel.Group = _context.Groups.Where(x => x.Name == "Helpdesk").FirstOrDefault();
            incidentModel.StatusName = incidentModel.Status.Name;
            //incidentModel.GroupName = incidentModel.Group.Name;
            incidentModel.RoleName = _context.Roles.Where(x => x.Name == "Helpdesk").Select(x => x.Name).FirstOrDefault();
            incidentModel.Category = _context.Categories.Where(x => x.Name == incidentModel.CategoryName).FirstOrDefault();
            incidentModel.Priority = _context.Priorities.Where(x => x.Name == incidentModel.PriorityName).FirstOrDefault();
            incidentModel.Department = _context.Departments.Where(x => x.Name == incidentModel.DepartmentName).FirstOrDefault();
            //_context.Add(incidentModel);
            
            _context.Incidents.Add(incidentModel);
            return _context.SaveChanges() > 0;
        }

        public IncidentModel Get(int id)
        {
            return _context.Incidents.SingleOrDefault(b => b.Id == id);
        }

        public IList<IncidentModel> GetAll()
        {
            
            return _context.Incidents.ToList();
        }

        public bool Update(IncidentModel model)
        {
            var editmodel = _context.Incidents.Where(x => x.Id == model.Id).FirstOrDefault();
            model.IncidentDescription = editmodel.IncidentDescription;
            model.LastModified = editmodel.LastModified;
            //model.ListCategories = editmodel.ListCategories;
            //model.ListDepartments = editmodel.ListDepartments;
            //model.ListPriorities = editmodel.ListPriorities;
            model.ModifiedBy = editmodel.ModifiedBy;
            model.Priority = editmodel.Priority;
            model.PriorityName = editmodel.PriorityName;
            model.Status = editmodel.Status;
            model.StatusName = editmodel.StatusName;
            model.Title = editmodel.Title;
             
            

            _context.Incidents.Update(model);
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
