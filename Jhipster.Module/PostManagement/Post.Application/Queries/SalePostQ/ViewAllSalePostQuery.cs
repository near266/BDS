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

namespace Post.Application.Queries.SalePostQ
{
    public class ViewAllSalePostQuery : IRequest<PagedList<SalePost>>
    {
        [JsonIgnore]
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public int? Status { get; set; }
        public int? Type { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class ViewAllSalePostQueryHandler : IRequestHandler<ViewAllSalePostQuery, PagedList<SalePost>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public ViewAllSalePostQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedList<SalePost>> Handle(ViewAllSalePostQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SearchSalePost(request.UserId, request.Title, request.Status,request.Type, request.Page, request.PageSize);
        }
    }
}
