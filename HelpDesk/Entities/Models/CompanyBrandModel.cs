using System;
using System.Collections.Generic;

namespace HelpDesk.Entities.Models
{
    public partial class CompanyBrandModel
    {
        public string BrandId { get; set; }
        public string CompanyId { get; set; }
        public string BrandName { get; set; }

        public virtual CompanyModel Company { get; set; }
    }
}
