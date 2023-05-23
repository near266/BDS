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
        public string? Keyword { get; set; }
        public int? FromPrice { get; set; }
        public int? ToPrice { get; set; }
        public double? FromArea { get; set; }
        public double? ToArea { get; set; }
        public string? Region { get; set; }
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
            return await _repository.GetShowingSalePost(request.Keyword,request.FromPrice, request.ToPrice, request.FromArea, request.ToArea, request.Region, request.Page, request.PageSize);
        }
    }
}
