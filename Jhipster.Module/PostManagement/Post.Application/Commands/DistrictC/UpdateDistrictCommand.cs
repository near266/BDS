using AutoMapper;
using MediatR;
using Post.Application.Commands.NewPostC;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Commands.DistrictC
{
    public class UpdateDistrictCommand: IRequest<int>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public string? LastModifiedBy { get; set; }

        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
    }
    public class UpdateDistrictCommandHandler : IRequestHandler<UpdateDistrictCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public UpdateDistrictCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<District>(request);
            return await _repository.UpdateDistrict(map, cancellationToken);
        }
    }
}
