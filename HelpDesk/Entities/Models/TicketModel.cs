using System;
using System.Collections.Generic;

namespace HelpDesk.Entities.Models
{
    public partial class TicketModel
    {
        public string TicketId { get; set; }
        public string CompanyId { get; set; }
        public string ProductId { get; set; }
        public string ModuleId { get; set; }
        public string BrandId { get; set; }
        public string CategoryId { get; set; }
        public string TktSubject { get; set; }
        public string TktContent { get; set; }
        public string TktStatus { get; set; }
        public string TktPriority { get; set; }
        public string TktCreatedBy { get; set; }
        public string TktAssignedTo { get; set; }
        public DateTime TktCreatedDate { get; set; }
        public DateTime? TktClosedDate { get; set; }
        public DateTime? TktReopenedDate { get; set; }
        public DateTime? TktFirstResponseDate { get; set; }
        public string TktAttachment { get; set; }
        public string TktRating { get; set; }
    }
}
