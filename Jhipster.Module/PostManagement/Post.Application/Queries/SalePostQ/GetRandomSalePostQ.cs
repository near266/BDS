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
    public class GetRandomSalePostQ : IRequest<List<SalePost>>
    {
        public int random { get; set; }
        public string? Region { get; set; }
    }
    public class GetRandomSalePostQHandler : IRequestHandler<GetRandomSalePostQ, List<SalePost>>
    {
        private readonly IPostRepository _repository;
        public GetRandomSalePostQHandler(IPostRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<SalePost>> Handle(GetRandomSalePostQ request, CancellationToken cancellationToken)
        {
            return await _repository.GetRandomSalePost(request.random, request.Region);
        }
    }
}
