using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models.Employee
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Dep { get; set; }
    }
}
