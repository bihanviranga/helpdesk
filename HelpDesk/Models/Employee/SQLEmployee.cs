using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models.Employee
{
    public class SQLEmployee : IEmployee
    {
        private readonly AppDbContext context;
        public SQLEmployee(AppDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            return context.Employees;
        }
    }
}
