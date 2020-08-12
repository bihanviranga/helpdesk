using System;
using System.Collections.Generic;

namespace HelpDesk.Entities.Models
{
    public partial class CompanyModel
    {
        public CompanyModel()
        {
            TktCategory = new HashSet<CategoryModel>();
            TktCompanyBrand = new HashSet<CompanyBrandModel>();
            TktModule = new HashSet<ModuleModel>();
            TktProduct = new HashSet<ProductModel>();
            TktTicketMasterCompany = new HashSet<TicketModel>();
            TktTicketMasterTktCreatedByCompanyNavigation = new HashSet<TicketModel>();
            TktUser = new HashSet<UserModel>();
        }

        public string CompanyId { get; set; }
        public string CompanyName { get; set; }

        public virtual ICollection<CategoryModel> TktCategory { get; set; }
        public virtual ICollection<CompanyBrandModel> TktCompanyBrand { get; set; }
        public virtual ICollection<ModuleModel> TktModule { get; set; }
        public virtual ICollection<ProductModel> TktProduct { get; set; }
        public virtual ICollection<TicketModel> TktTicketMasterCompany { get; set; }
        public virtual ICollection<TicketModel> TktTicketMasterTktCreatedByCompanyNavigation { get; set; }
        public virtual ICollection<UserModel> TktUser { get; set; }
    }
}
