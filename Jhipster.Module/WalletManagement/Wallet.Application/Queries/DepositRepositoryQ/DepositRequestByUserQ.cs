using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.DepositRepositoryQ
{
    public class DepositRequestByUserQ:IRequest<List<DepositRequest>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
    public class DepositRequestByUserQHandler : IRequestHandler<DepositRequestByUserQ, List<DepositRequest>>
    {
        private readonly IDepositRequestRepository _repository;
        public DepositRequestByUserQHandler(IDepositRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DepositRequest>> Handle(DepositRequestByUserQ request, CancellationToken cancellationToken)
        {
            return await _repository.GetByUser(request.Id);

        }
    }
}
