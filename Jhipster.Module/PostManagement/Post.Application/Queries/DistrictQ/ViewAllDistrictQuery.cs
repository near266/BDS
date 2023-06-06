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
    public class ViewAllDistrictQuery : IRequest<PagedList<District>>
    {
        public string? Name { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class ViewAllDistrictQueryHandler : IRequestHandler<ViewAllDistrictQuery, PagedList<District>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public ViewAllDistrictQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedList<District>> Handle(ViewAllDistrictQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SearchDistrict(request.Name, request.Page, request.PageSize);
        }
    }
}
