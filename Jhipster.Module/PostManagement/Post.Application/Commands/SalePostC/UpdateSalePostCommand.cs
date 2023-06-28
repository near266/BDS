using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Commands.SalePostC
{
    public class UpdateSalePostCommand : IRequest<int>
    {
        public string Id { get; set; }
        public int Type { get; set; }
        public int? Status { get; set; }
        [MinLength(30)]
        [MaxLength(100)]
        public string? Titile { get; set; }
        [MaxLength(3000)]
        public string? Description { get; set; }
        public string? Region { get; set; }
        public string? Ward { get; set; }
        public List<string>? Image { get; set; }
        public double Price { get; set; }
        public double? Area { get; set; }
        public int IsOwner { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public double? NumberOfDate { get; set; }
        public string? Reason { get; set; }
        public List<string>? Documents { get; set; }
        public int Unit { get; set; }

        public DateTime? ChangeDate { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
    }
    public class UpdateSalePostCommandHandler : IRequestHandler<UpdateSalePostCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public UpdateSalePostCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateSalePostCommand request, CancellationToken cancellationToken)
        {
            //var map = _mapper.Map<SalePost>(request);
            return await _repository.UpdateSalePost(request, request.NumberOfDate, cancellationToken);
        }
    }
}
