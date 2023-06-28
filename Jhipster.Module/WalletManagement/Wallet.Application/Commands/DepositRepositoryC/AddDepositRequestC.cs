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

namespace Wallet.Application.Commands.DepositRepositoryC
{
    public class AddDepositRequestC : IRequest<int>
    {
        [JsonIgnore]
        public Guid CustomerId { get; set; }
        public decimal? Amount { get; set; }

        public string? PhoneNumber { get; set; }
        public string Image { get; set; }
        [JsonIgnore]
        public int Status { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    }
    public class AddDepositRequestCHandler : IRequestHandler<AddDepositRequestC, int>
    {
        private readonly IDepositRequestRepository _repository;
        private readonly IMapper _mapper;
        public AddDepositRequestCHandler(IDepositRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddDepositRequestC request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<DepositRequest>(request);
            return await _repository.Add(map);
        }
    }
}
