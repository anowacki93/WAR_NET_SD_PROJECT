using Microsoft.AspNetCore.Identity;
using SDMan.Context;
using SDMan.Models;
using SDMan.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDMan.Services
{
    public class IncidentService : IIncidentService
    {
        
        private readonly SDManDbContext _context;
        public IncidentService(SDManDbContext context)
        {
            _context = context;
        }
        public bool Create(IncidentModel incidentModel)
        {
            
            incidentModel.LastModified = DateTime.UtcNow.AddHours(1);
            incidentModel.CreatedDate = DateTime.UtcNow.AddHours(1);
            incidentModel.Status = _context.Statuses.FirstOrDefault();
            incidentModel.StatusId = incidentModel.Status.Id;
            _context.Add(incidentModel);
            
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
