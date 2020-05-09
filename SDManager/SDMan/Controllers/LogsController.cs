using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SDMan.Context;
using SDMan.Models;
using SDMan.Services.Interfaces;

namespace SDMan.Controllers
{
    
    public class LogsController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly ILogsSerivce _modelService;
        private readonly IIncidentService _incidentService;
        
        private readonly SDManDbContext _context;
        IncidentModel _modelTest = new IncidentModel();
        public LogsController(ILogsSerivce modelService, SDManDbContext context, UserManager<UserModel> userManager, IIncidentService incidentService)
        {
            
            _modelService = modelService;
            _context = context;
            _userManager = userManager;
            _incidentService = incidentService;

        }
        public IActionResult Logs(int id)
        {
            return View(_modelService.Get(id));
        }
    }
}