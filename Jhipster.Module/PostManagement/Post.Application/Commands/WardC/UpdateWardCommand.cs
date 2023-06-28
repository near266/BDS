using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Commands.WardC
{
    public class UpdateWardCommand : IRequest<int>
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? DistrictId { get; set; }
        public string? Description { get; set; }
        public int? Order { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }

        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
    }
    public class UpdateWardCommandHandler : IRequestHandler<UpdateWardCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public UpdateWardCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateWardCommand request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateWard(request, cancellationToken);
        }
    }
}
