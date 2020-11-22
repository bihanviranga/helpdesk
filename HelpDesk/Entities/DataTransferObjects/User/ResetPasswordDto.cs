using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.DataTransferObjects.User
{
    public class ResetPasswordDto
    {
        public string CompanyId { get; set; }
        public string UserName { get; set; }
    }
}
