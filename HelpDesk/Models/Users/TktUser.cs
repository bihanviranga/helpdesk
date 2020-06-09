using System;
using System.Collections.Generic;

namespace HelpDesk.Model
{
    public partial class TktUser
    {
        public string CompanyId { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string UserImage { get; set; }
        public string UserRole { get; set; }
    }
}
