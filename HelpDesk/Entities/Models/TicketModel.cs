using System;
using System.Collections.Generic;

namespace HelpDesk.Entities.Models
{
    public partial class TicketModel
    {
        public TicketModel()
        {
            TktConversation = new HashSet<ConversationModel>();
            TktNotification = new HashSet<NotificationModel>();
            TktTicketOperator = new HashSet<TicketOperatorModel>();
            TktTicketTimeline = new HashSet<TicketTimelineModel>();
        }

        public string TicketId { get; set; }
        public string TicketCode { get; set; }
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
        public string TktCreatedByCompany { get; set; }
        public string TktAssignedTo { get; set; }
        public DateTime TktCreatedDate { get; set; }
        public DateTime? TktClosedDate { get; set; }
        public DateTime? TktReopenedDate { get; set; }
        public DateTime? TktFirstResponseDate { get; set; }
        public string TktAttachment { get; set; }
        public string TktRating { get; set; }

        public virtual CompanyModel Company { get; set; }
        public virtual UserModel TktAssignedToNavigation { get; set; }
        public virtual CompanyModel TktCreatedByCompanyNavigation { get; set; }
        public virtual UserModel TktCreatedByNavigation { get; set; }
        public virtual ICollection<ConversationModel> TktConversation { get; set; }
        public virtual ICollection<NotificationModel> TktNotification { get; set; }
        public virtual ICollection<TicketOperatorModel> TktTicketOperator { get; set; }
        public virtual ICollection<TicketTimelineModel> TktTicketTimeline { get; set; }
    }
}
