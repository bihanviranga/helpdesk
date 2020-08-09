using System;
using System.Collections.Generic;

namespace HelpDesk.Entities.Models
{
    public partial class UserModel
    {
        public UserModel()
        {
            TktArticle = new HashSet<ArticleModel>();
            TktConversation = new HashSet<ConversationModel>();
            TktNotification = new HashSet<NotificationModel>();
            TktResTemplate = new HashSet<ResTemplateModel>();
            TktTicketMasterTktAssignedToNavigation = new HashSet<TicketModel>();
            TktTicketMasterTktCreatedByNavigation = new HashSet<TicketModel>();
            TktTicketOperatorAssignedByNavigation = new HashSet<TicketOperatorModel>();
            TktTicketOperatorTktOperatorNavigation = new HashSet<TicketOperatorModel>();
            TktTicketTimeline = new HashSet<TicketTimelineModel>();
        }

        public string CompanyId { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string UserImage { get; set; }
        public string UserRole { get; set; }

        public virtual CompanyModel Company { get; set; }
        public virtual ICollection<ArticleModel> TktArticle { get; set; }
        public virtual ICollection<ConversationModel> TktConversation { get; set; }
        public virtual ICollection<NotificationModel> TktNotification { get; set; }
        public virtual ICollection<ResTemplateModel> TktResTemplate { get; set; }
        public virtual ICollection<TicketModel> TktTicketMasterTktAssignedToNavigation { get; set; }
        public virtual ICollection<TicketModel> TktTicketMasterTktCreatedByNavigation { get; set; }
        public virtual ICollection<TicketOperatorModel> TktTicketOperatorAssignedByNavigation { get; set; }
        public virtual ICollection<TicketOperatorModel> TktTicketOperatorTktOperatorNavigation { get; set; }
        public virtual ICollection<TicketTimelineModel> TktTicketTimeline { get; set; }
    }
}
