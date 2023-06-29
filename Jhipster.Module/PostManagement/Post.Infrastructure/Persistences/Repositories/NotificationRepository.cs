using Jhipster.Crosscutting.Utilities;
using Jhipster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.Persistences.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDatabaseContext _databaseContext;
        public NotificationRepository(ApplicationDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<int> CreateNotification(Notification rq, CancellationToken cancellationToken)
        {
            rq.IsSeen = false;
            rq.CreatedDate = DateTime.Now;
            await _databaseContext.Notification.AddAsync(rq);
            return await _databaseContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<PagedList<Notification>> GetAllNotifications(string Id, int Page, int PageSize)
        {
            var reponse = new PagedList<Notification>();
            var data = await _databaseContext.Notification.Where(i => i.UserId == Id).OrderByDescending(i => i.CreatedDate).ToListAsync();
            reponse.TotalCount = data.Count;
            reponse.Data = data.Skip(PageSize * (Page - 1))
                                    .Take(PageSize)
                                    .ToList();
            return reponse;
        }

        public async Task<int> UpdateNotification(Guid Id, CancellationToken cancellationToken)
        {
            var check = await _databaseContext.Notification.FirstOrDefaultAsync(i => i.Id == Id);
            if (check == null) throw new Exception("Fail");
            else
            {
                check.IsSeen = true;
                return await _databaseContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
