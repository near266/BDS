using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
    public class AddDistrictCommand:IRequest<int>
    {
        public string Name { get;set; }

        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }
    }
    public class AddDistrictCommnandHandler : IRequestHandler<AddDistrictCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public AddDistrictCommnandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddDistrictCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<District>(request);
            return await _repository.AddDistrict(map, cancellationToken);
        }
    }
}
