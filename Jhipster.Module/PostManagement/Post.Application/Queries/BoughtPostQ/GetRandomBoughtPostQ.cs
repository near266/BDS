using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using MediatR;
using Post.Application.Contracts;
using Post.Application.DTO;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Queries.BoughtPostQ
{
    public class GetRandomBoughtPostQ : IRequest<List<BoughtPost>>
    {
        public int random { get; set; }
        public string? Region { get; set; }
    }
    public class GetRandomBoughtPostQHandler : IRequestHandler<GetRandomBoughtPostQ, List<BoughtPost>>
    {
        private readonly IPostRepository _repository;
        public GetRandomBoughtPostQHandler(IPostRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<BoughtPost>> Handle(GetRandomBoughtPostQ request, CancellationToken cancellationToken)
        {
            return await _repository.GetRandomBoughtPost(request.random, request.Region);
        }
    }
}
