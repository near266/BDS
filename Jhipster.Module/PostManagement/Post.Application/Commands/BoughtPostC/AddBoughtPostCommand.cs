﻿using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Post.Application.Commands.BoughtPostC
{
    public class AddBoughtPostCommand : IRequest<int>
    {
        public string Titile { get; set; }
        public string? Description { get; set; }
        public List<string>? Image { get; set; }
        public string? Region { get; set; }
        public string? Ward { get; set; }
        public double Price { get; set; }
        public bool? IsOpenFinance { get; set; }
        public string? LandToBuy { get; set; }
        public string? Criteria { get; set; }
        public string? FullName { get; set; }
        [JsonIgnore]
        public string? Username { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public double? PriceTo { get; set; }
        public string? RangePrice { get; set; }

        public string? Email { get; set; }
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
    }
    public class AddBoughtPostCommandHandler : IRequestHandler<AddBoughtPostCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public AddBoughtPostCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddBoughtPostCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<BoughtPost>(request);
            var check = await _repository.CheckTitle(request.Titile, request.UserId);
            if (!check) throw new ArgumentException("Can not have the same title !");
            return await _repository.AddBoughtPost(map, cancellationToken);
        }
    }
}
