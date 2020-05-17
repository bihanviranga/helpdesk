using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models.Employee
{
    public interface IEmployee
    {
        IEnumerable<EmployeeModel> GetAllEmployees();
    }
}
