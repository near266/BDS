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

namespace Post.Application.Commands.SalePostC
{
    public class RepostSalePostCommand : IRequest<int>
    {
        public string Id { get; set; }
        public int Type { get; set; }
        public double NumberOfDate { get; set; }
        [JsonIgnore]
        public string? Username { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
    }
    public class RepostSalePostCommandHandler : IRequestHandler<RepostSalePostCommand,int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public RepostSalePostCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(RepostSalePostCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<SalePost>(request);
            return await _repository.RepostSalePost(request.Id,request.Type,request.NumberOfDate,cancellationToken);
            
        }
    }

}
