using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Post.Application.Commands.BoughtPostC
{
    public class AddBoughtPostCommand : IRequest<int>
    {
        public string? Titile { get; set; }
        public string? Description { get; set; }
        public string? Region { get; set; }
        public double Price { get; set; }
        public bool? IsOpenFinance { get; set; }
        public string? Unit { get; set; }
        public string? LandToBuy { get; set; }
        public string? Criteria { get; set; }
        public string? FullName { get; set; }
        [JsonIgnore]
        public string? Username { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
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
            return await _repository.AddBoughtPost(map, cancellationToken);
        }
    }
}
