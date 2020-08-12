using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Entities.DataTransferObjects.Ticket
{
    public class CreateTicketDto
    {
        public String CompanyId { get; set; }
        public String ProductId { get; set; }
        public String ModuleId { get; set; }
        public String BrandId { get; set; }
        public String CategoryId { get; set; }
        public String TktSubject { get; set; }
        public String TktContent { get; set; }
        public string TktPriority { get; set; }
        public String TktStatus { get; set; }
        public String TktCreatedBy { get; set; }
        public string TktCreatedByCompany { get; set; }
        public DateTime TktCreatedDate { get; set; }
        public String TktAttachment { get; set; }

    }
}
