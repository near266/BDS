using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Queries.SalePostQ
{
    public class GetAllShowingSalePostQuery : IRequest<PagedList<SalePost>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllShowingSalePostQueryHandler : IRequestHandler<GetAllShowingSalePostQuery, PagedList<SalePost>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public GetAllShowingSalePostQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedList<SalePost>> Handle(GetAllShowingSalePostQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetShowingSalePost(request.Page, request.PageSize);
        }
    }
}
