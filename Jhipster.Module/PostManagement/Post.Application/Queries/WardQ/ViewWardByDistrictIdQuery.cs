using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using Post.Application.DTO;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Queries.WardQ
{
    public class ViewWardByDistrictIdQuery : IRequest<List<WardDTO>>
    {
        public string? DistrictId { get; set; }
        public string? Name { get; set;}
        
    }
    public class GetWardsByDistrictQHandler : IRequestHandler<ViewWardByDistrictIdQuery,List<WardDTO>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public GetWardsByDistrictQHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<WardDTO>> Handle(ViewWardByDistrictIdQuery request, CancellationToken cancellationToken)
        {
            var res =  await _repository.SearchWardByDistrict(request.DistrictId,request.Name);
            var map = _mapper.Map<List<WardDTO>>(res);
            return map;
        }
    }
}
