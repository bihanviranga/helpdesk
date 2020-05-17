using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HelpDesk.Models;

namespace HelpDesk.Controllers
{
    public class HomeController : Controller
    {

        public JsonResult Index()
        {
            return Json("Lets Dev Help Desk");
        }

        
    }
}
