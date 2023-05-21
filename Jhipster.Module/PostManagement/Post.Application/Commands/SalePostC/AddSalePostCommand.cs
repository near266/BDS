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
        //đơn vị : triệu, tỷ, . . .
        public string? Unit { get; set; }
        //Diện tích
        public double? Area { get; set; }
        //khu vực
        public string? Region { get; set; }
        public int Status { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
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
            map.DueDate = map.CreatedDate.AddDays(25);
            var check = await _repository.CheckBalance(request.UserId,request.Type);
            if(!check) throw new ArgumentException ("Not enough money");
            return await _repository.AddSalePost(map, cancellationToken);
        }
    }
}
