using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.CustomerC
{
    public class UpdateCustomerCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string? firstName { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Company { get; set; }
        public bool? IsUnique { get; set; }
        public string? Avatar { get; set; }
        //Sàn
        public string? Exchange { get; set; }
        public string? ExchangeDescription { get; set; }
        public DateTime? MaintainFrom { get; set; }
        public DateTime? MaintainTo { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
    }
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, int>
    {
        private readonly ICustomerRepository _repo;
        private readonly IMapper _mapper;
        public UpdateCustomerCommandHandler(ICustomerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<Customer>(request);
            if(request.firstName != null)
            {
                map.CustomerName = request.firstName;
            }    
            var res = await _repo.Update(map, cancellationToken);
            return res;
        }
    }
}
