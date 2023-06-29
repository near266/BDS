using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.NotificationC
{
    public class CreateNotificationCommand : IRequest<int>
    {
        public string Content { get; set; }

        public bool IsSeen { get; set; }
        public string UserId { get; set; }
    }

    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, int>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        public CreateNotificationCommandHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var map = new Notification();
            map.Content = request.Content;
            map.IsSeen = request.IsSeen;
            map.UserId = request.UserId;
            return await _notificationRepository.CreateNotification(map, cancellationToken);
        }
    }
}
