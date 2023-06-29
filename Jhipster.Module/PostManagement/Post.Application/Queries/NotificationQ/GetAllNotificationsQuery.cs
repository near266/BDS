using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Queries.NotificationQ
{
    public class GetAllNotificationsQuery : IRequest<PagedList<Notification>>
    {
        [JsonIgnore]
        public string Id { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, PagedList<Notification>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        public GetAllNotificationsQueryHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<Notification>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            return await _notificationRepository.GetAllNotifications(request.Id, request.Page, request.Page);
        }
    }
}
