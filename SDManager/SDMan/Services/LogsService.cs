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
    
    public class LogsService : ILogsSerivce
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SDManDbContext _context;

        public LogsService(SDManDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public LogsModel Get(int id)
        {
            var Log = new LogsModel();
            var incidentModel = new IncidentModel();
            incidentModel = _context.Incidents.SingleOrDefault(b => b.Id == id);
            incidentModel.Logs = _context.Logs.SingleOrDefault(b => b.Id == incidentModel.LogId);
            incidentModel.Logs.User = _context.Users.SingleOrDefault(x => x.UserName == incidentModel.CreatedBy);

            //var logsModel = incidentModel.Logs;

            return incidentModel.Logs;
            
        }

        public IList<LogsModel> GetAll(int id)
        {
            var incidentModel = new IncidentModel();
            incidentModel = _context.Incidents.SingleOrDefault(b => b.Id == id);
            incidentModel.Logs = _context.Logs.SingleOrDefault(b => b.Id == incidentModel.LogId);
            incidentModel.Logs.User = _context.Users.SingleOrDefault(x => x.UserName == incidentModel.CreatedBy);
            
            return _context.Logs.Where(x=>x.Id==incidentModel.LogId).ToList();
        }

        
    }
}
