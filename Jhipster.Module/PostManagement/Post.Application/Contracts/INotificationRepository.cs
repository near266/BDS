using Jhipster.Crosscutting.Utilities;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Contracts
{
    public interface INotificationRepository
    {
        Task<int> CreateNotification(Notification rq, CancellationToken cancellationToken);
        Task<int> UpdateNotification(Guid Id, CancellationToken cancellationToken);
        Task<PagedList<Notification>> GetAllNotifications(string Id, int Page, int PageSize);

    }
}
