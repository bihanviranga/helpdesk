using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        public String Name { get; set; }
        
        public String Department { get; set; }
    }
}
