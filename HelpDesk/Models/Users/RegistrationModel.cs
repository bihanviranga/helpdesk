using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models.Users
{
    public class RegistrationModel
    {
        [Required]
        public String Email { get; set; }

        public String Name { get; set; }
        public String UserName { get; set; }
        public String PhoneNumber { get; set; }
        public String Department { get; set; }

        [DataType(DataType.Password)]
        public String Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }
        public String TokenAvailable { get; set; }
    }
}
