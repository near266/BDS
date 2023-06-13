using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Commands.CommonC
{
    public class ChangeStatusCommand : IRequest<int>
    {
        public string PostId { get; set; }
        public int PostType { get; set; }
        public int StatusType { get; set; }

        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
    }
    public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public ChangeStatusCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<int> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
        {
            return await _repository.ChangeStatus(request.PostId, request.PostType, request.StatusType,request.LastModifiedDate, request.LastModifiedBy,cancellationToken);
        }
    }
}
