using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;

namespace Wallet.Application.Commands.CustomerC
{
    public class DeleteCustomerCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, int>
    {
        private readonly ICustomerRepository _repo;
        public DeleteCustomerCommandHandler(ICustomerRepository repo)
        {
            _repo = repo;
        }
        public async Task<int> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var res = await _repo.Delete(request.Id, cancellationToken);
            return res;
        }
    }
}
