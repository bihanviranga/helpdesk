using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.DataTransferObjects
{
    public class UserRegistrationDto
    {

        public String Email { get; set; }
        public String UserRole { get; set; }
        
        public String Phone { get; set; }
        public String FullName { get; set; }
        public String CompanyId { get; set; }
        public String UserType { get; set; }
        public String UserImage { get; set; }

        [DataType(DataType.Password)]
        public String Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }
        public String TokenAvailable { get; set; }
    }
}
