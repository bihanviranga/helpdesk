using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public JsonResult Index()
        {
            return Json("Lets Dev Help Desk");
        }

        [Authorize]
        [HttpGet]
        [Route("[controller]/AuthenticationCheck")]
        public JsonResult AuthenticationCheck()
        {
            return Json("Authenticated");
        }


    }
}
