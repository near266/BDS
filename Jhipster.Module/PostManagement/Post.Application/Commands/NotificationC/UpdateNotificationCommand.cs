using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.NotificationC
{
    public class UpdateNotificationCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }
    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, int>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        public UpdateNotificationCommandHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            return await _notificationRepository.UpdateNotification(request.Id, cancellationToken);
        }
    }
}
