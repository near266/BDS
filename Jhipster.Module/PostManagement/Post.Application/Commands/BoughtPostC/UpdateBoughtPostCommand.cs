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

namespace Post.Application.Commands.BoughtPostC
{
    public class UpdateBoughtPostCommand : IRequest<int>
    {
        public string Id { get; set; }
        public string? LandToBuy { get; set; }
        public string? Criteria { get; set; }

        public string? FullName { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
    }
    public class UpdateBoughtPostCommandHandler : IRequestHandler<UpdateBoughtPostCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public UpdateBoughtPostCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateBoughtPostCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<BoughtPost>(request);
            return await _repository.UpdateBoughtPost(map, cancellationToken);
        }
    }
}
