using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Models.Employee;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelpDesk.Controllers.Eployee
{
    public class EmployeeController : Controller
    {
        IEmployee _employee;
        public EmployeeController(IEmployee employee)
        {
            this._employee = employee;
        }
        public JsonResult Index()
        {
            return Json("Employee Page");
        }

        [HttpGet]
        public JsonResult getEmployee()
        {
            return Json(_employee.GetAllEmployees());
        }
    }
}
