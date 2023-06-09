using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;

namespace Wallet.Application.Commands.DepositRepositoryC
{
    public class UpdateDepositRequestC : IRequest<int>
    {
        public Guid Id { get; set; }
        public int Status { get; set; }
    }
    public class UpdateDepositRequestCHandler : IRequestHandler<UpdateDepositRequestC, int>
    {
        private readonly IDepositRequestRepository _repository;
        private readonly IMapper _mapper;
        public UpdateDepositRequestCHandler(IDepositRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateDepositRequestC request, CancellationToken cancellationToken)
        {
            return await _repository.Update(request.Id, request.Status,cancellationToken);
        }
    }
}
