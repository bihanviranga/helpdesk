using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Entities.Contracts;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Entities.Repository
{
    public class NotificationRepository : RepositoryBase<NotificationModel>, INotificationRepository
    {
        public NotificationRepository(HelpDeskContext helpDeskContext) : base(helpDeskContext) { }

        public void CreateNotification(string notificationType, string ticketId, string userId)
        {
            NotificationModel notification = new NotificationModel();
            notification.NotifId = Guid.NewGuid().ToString();
            notification.TicketId = ticketId;
            notification.NotifRead = false;
            notification.NotifDate = DateTime.Now;
            notification.NotifUser = userId;

            if (notificationType == "tktAssigned")
            {
                notification.NotifContent = "You have been assigned a new ticket.";
            }
            else if (notificationType == "tktReplied")
            {
                notification.NotifContent = "You have new messages in a ticket.";
            }

            Create(notification);
        }

        public void DeleteNotification(NotificationModel notification)
        {
            Delete(notification);
        }

        public async Task<NotificationModel> GetNotificationById(Guid id)
        {
            return await FindByCondition(notif => notif.NotifId.Equals(id.ToString())).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<NotificationModel>> GetNotificationsForUser(string userId)
        {
            return await FindByCondition(notif => notif.NotifUser.Equals(userId)).ToListAsync();
        }

        public void UpdateNotification(NotificationModel notification)
        {
            Update(notification);
        }
    }
}