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

namespace Post.Application.Queries.WardQ
{
    public class ViewWardByDistrictIdQuery : IRequest<List<Ward>>
    {
        public string? DistrictId { get; set; }
        
    }
    public class GetWardsByDistrictQHandler : IRequestHandler<ViewWardByDistrictIdQuery,List<Ward>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public GetWardsByDistrictQHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Ward>> Handle(ViewWardByDistrictIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SearchWardByDistrict(request.DistrictId);
        }
    }
}
