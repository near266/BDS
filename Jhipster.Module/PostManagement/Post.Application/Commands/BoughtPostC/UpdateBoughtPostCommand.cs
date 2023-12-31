﻿using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Commands.BoughtPostC
{
    public class UpdateBoughtPostCommand : IRequest<int>
    {
        public string Id { get; set; }
        public string? Titile { get; set; }
        public string? Description { get; set; }
        public List<string>? Image { get; set; }
        public string? Region { get; set; }
        public string? Ward { get; set; }
        public double Price { get; set; }
        public bool? IsOpenFinance { get; set; }
        public string? LandToBuy { get; set; }
        public string? Criteria { get; set; }
        public int? Status { get; set; }
        public string? FullName { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }
        public double? PriceTo { get; set; }
        public string? RangePrice { get; set; }

        public string? Email { get; set; }
        public DateTime? ChangeDate { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
    }
    public class UpdateBoughtPostCommandHandler : IRequestHandler<UpdateBoughtPostCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public UpdateBoughtPostCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateBoughtPostCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<BoughtPost>(request);
            return await _repository.UpdateBoughtPost(map, cancellationToken);
        }
    }
}
