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

namespace Post.Application.Queries.BoughtPostQ
{
    public class GetAllShowingBoughtPostQuery : IRequest<PagedList<BoughtPost>>
    {
        public string? UserId { get; set; }
        public string? Keyword { get; set; }
        public double? FromPrice { get; set; }
        public double? ToPrice { get; set; }
        public string? Region { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllShowingBoughtPostQueryHandler : IRequestHandler<GetAllShowingBoughtPostQuery, PagedList<BoughtPost>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public GetAllShowingBoughtPostQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedList<BoughtPost>> Handle(GetAllShowingBoughtPostQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetShowingBoughtPost(request.UserId,request.Keyword, request.FromPrice, request.ToPrice, request.Region, request.Page, request.PageSize);
        }
    }
}
