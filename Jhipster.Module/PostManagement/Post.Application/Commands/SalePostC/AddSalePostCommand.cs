using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Post.Application.Commands.SalePostC
{
    public class AddSalePostCommand : IRequest<int>
    {
        public int Type { get; set; }
        public string? Titile { get; set; }
        public string? Description { get; set; }

        public List<string>? Image { get; set; }

        public int IsOwner { get; set; }
        [JsonIgnore]
        public string? Username { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
        public double Price { get; set; }
        //Diện tích
        public double? Area { get; set; }
        //khu vực
        public string? Region { get; set; }
        public string? Ward { get; set; }
        public int Status { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public double NumberOfDate { get; set; }
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
    }
    public class AddSalePostCommandHadler : IRequestHandler<AddSalePostCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public AddSalePostCommandHadler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddSalePostCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<SalePost>(request);
            map.DueDate = map.CreatedDate.AddDays(request.NumberOfDate);
            var check2 = await _repository.CheckBalancePromotional(request.UserId, request.Type, request.NumberOfDate);
            var check = await _repository.CheckBalance(request.UserId, request.Type, request.NumberOfDate);
            if (!check && !check2) throw new ArgumentException("Not enough money");
            return await _repository.AddSalePost(map, check, check2, request.NumberOfDate, cancellationToken);
        }
    }
}
