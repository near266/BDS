using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
namespace Post.Application.Commands.BoughtPostC
{
    public class AddBoughtPostCommand : IRequest<int>
    {
        public string? LandToBuy { get; set; }
        public string? Criteria { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
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
