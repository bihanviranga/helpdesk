using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Entities.Models;

namespace HelpDesk.Entities.Contracts
{
    public interface INotificationRepository : IRepositoryBase<NotificationModel>
    {
        Task<IEnumerable<NotificationModel>> GetNotificationsForUser(Guid userId);
        Task<NotificationModel> GetNotificationById(Guid id);
        void CreateNotification(NotificationModel notification);
        void UpdateNotification(NotificationModel notification);
        void DeleteNotification(NotificationModel notification);
    }
}