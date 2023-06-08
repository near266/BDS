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
    public class AddWardCommand: IRequest<int>
    {
        public string? Name { get; set; }
        public string? DistrictId { get; set; }
        public string? Description { get; set; }
        public int? Order { get; set; }

        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }
    }
    public class AddNewWardCommandHandler : IRequestHandler<AddWardCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public AddNewWardCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddWardCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<Ward>(request);
            return await _repository.AddWard(map, cancellationToken);
        }
    }
}
