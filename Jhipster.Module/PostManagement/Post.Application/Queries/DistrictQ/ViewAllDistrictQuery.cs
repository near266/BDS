using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Queries.DistrictQ
{
    public class ViewAllDistrictQuery : IRequest<List<District>>
    {
    }
    public class ViewAllDistrictQueryHandler : IRequestHandler<ViewAllDistrictQuery, List<District>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public ViewAllDistrictQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<District>> Handle(ViewAllDistrictQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SearchDistrict();
        }
    }
}
