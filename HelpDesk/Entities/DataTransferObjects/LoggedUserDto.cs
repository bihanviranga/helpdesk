using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.DataTransferObjects
{
    public class LoggedUserDto
    {
        public string CompanyId { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserImage { get; set; }
        public string UserRole { get; set; }
        public string Token { get; set; }
    }
}
