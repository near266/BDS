using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Post.Application.Contracts;
using Post.Application.DTO.SalePostDtos;
using Post.Domain.Entities;

namespace Post.Application.Queries.SalePostQ
{
    public class ViewDetailSalePostQuery : IRequest<DetailSalePost>
    {
        public string Id { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
    }
    public class ViewDeatailSalePostQueryHandler : IRequestHandler<ViewDetailSalePostQuery, DetailSalePost>
    {
        private readonly IPostRepository _Repository;
        private readonly IMapper _mapper;
        public ViewDeatailSalePostQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _Repository = repository;
            _mapper = mapper;
        }

        public async Task<DetailSalePost> Handle(ViewDetailSalePostQuery request, CancellationToken cancellationToken)
        {
            return await _Repository.ViewDetailSalePost(request.Id, request.UserId);
        }
    }
}
