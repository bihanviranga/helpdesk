using System;
using System.Collections.Generic;

namespace HelpDesk.Entities.Models
{
    public partial class ModuleModel
    {
        public string ModuleId { get; set; }
        public string CompanyId { get; set; }
        public string ModuleName { get; set; }

        public virtual CompanyModel Company { get; set; }
    }
}
